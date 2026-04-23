-- ============================================================
-- GESTIÓN DE DISPONIBILIDAD DE OPERADORES
-- ============================================================

-- ── fn_ListDispOperador ────────────────────────────────────────
-- Retorna todos los slots ACTIVOS del operador y columnas de resumen.
-- Si no tiene disponibilidad registrada, retorna 0 filas.
DROP FUNCTION IF EXISTS fn_ListDispOperador(INT);
CREATE OR REPLACE FUNCTION fn_ListDispOperador(_IdOperador INT)
RETURNS TABLE(
    "Id"                  INT,
    "NroDia"              SMALLINT,
    "NombreDia"           VARCHAR(9),
    "HoraInicio"          TIME,
    "HoraFin"             TIME,
    "TotalHorasSemanales" INT,
    "DiasActivos"         INT
)
LANGUAGE plpgsql AS $$
BEGIN
    RETURN QUERY
    WITH resumen AS (
        SELECT
            COALESCE(
                SUM(EXTRACT(EPOCH FROM (d2."HoraFin" - d2."HoraInicio")) / 3600)::INT,
                0
            )                              AS total_horas,
            COUNT(DISTINCT d2."NroDia")::INT AS dias_activos
        FROM "Disponibilidad" d2
        WHERE d2."IdOperador" = _IdOperador
          AND d2."Estado"     = 'ACTIVO'
    )
    SELECT
        d."Id"::INT,
        d."NroDia",
        d."NombreDia",
        d."HoraInicio",
        d."HoraFin",
        r.total_horas::INT,
        r.dias_activos::INT
    FROM   "Disponibilidad" d
    CROSS  JOIN resumen r
    WHERE  d."IdOperador" = _IdOperador
      AND  d."Estado"     = 'ACTIVO'
    ORDER  BY d."NroDia", d."HoraInicio";
END;
$$;


-- ── sp_AsignarDispOperador ─────────────────────────────────────
-- Guarda / actualiza la disponibilidad semanal de un operador.
--
-- Parámetros de entrada:
--   _IdOperador     → ID del operador
--   _Disponibilidad → JSON con array de rangos:
--                     [{"NroDia":2,"NombreDia":"Lun","HoraInicio":"08:00","HoraFin":"13:00"}, ...]
--   _Confirmar      → FALSE = verificar conflictos y retornar warning si los hay
--                     TRUE  = aplicar cambios aunque haya conflictos
--   _ActualizadoPor → ID del usuario que realiza la operación
--
-- Valores de salida _Exitoso:
--   0 → Error (no se guardó)
--   1 → Éxito
--   3 → Advertencia: hay conflictos, requiere confirmación del usuario
--
-- Nota: al confirmar con conflictos, las reservas ASIGNADAS fuera del nuevo
-- horario pasan a EstadoOperacion = 'RESERVADO' y se libera operador + grúa.
-- ('PENDIENTE' no es un valor válido del CHECK constraint de EstadoOperacion)

DROP PROCEDURE IF EXISTS sp_AsignarDispOperador(INT, TEXT, BOOLEAN, INT, INT, TEXT, TEXT);
CREATE OR REPLACE PROCEDURE sp_AsignarDispOperador(
    _IdOperador      INT,
    _Disponibilidad  TEXT,
    _Confirmar       BOOLEAN,
    _ActualizadoPor  INT,
    INOUT _Exitoso   INT,
    INOUT _Mensaje   TEXT,
    INOUT _Conflictos TEXT
)
LANGUAGE plpgsql AS $$
DECLARE
    v_DispJson     JSONB;
    v_ConflictoArr JSONB;
    v_Count        INT;
BEGIN
    _Exitoso    := 0;
    _Mensaje    := '';
    _Conflictos := '[]';

    -- Parsear JSON
    v_DispJson := _Disponibilidad::JSONB;

    -- Detectar reservas ASIGNADAS que quedarían fuera del nuevo horario
    SELECT COALESCE(
        jsonb_agg(
            jsonb_build_object(
                'IdReserva',     r."Id",
                'FechaServicio', to_char(r."FechaServicio", 'YYYY-MM-DD'),
                'HoraInicio',    to_char(r."HoraInicio", 'HH24:MI')
            )
        ),
        '[]'::JSONB
    )
    INTO v_ConflictoArr
    FROM "Reserva" r
    WHERE r."IdOperador"      = _IdOperador
      AND r."EstadoOperacion" = 'ASIGNADO'
      AND r."Estado"          = 'ACTIVO'
      AND NOT EXISTS (
          SELECT 1
          FROM   jsonb_array_elements(v_DispJson) jd
          WHERE  (jd->>'NroDia')::SMALLINT =
                     (EXTRACT(DOW FROM r."FechaServicio")::INT + 1)::SMALLINT
            AND  (jd->>'HoraInicio')::TIME <= r."HoraInicio"
            AND  (jd->>'HoraFin')::TIME    >= r."HoraFin"
      );

    v_Count := jsonb_array_length(v_ConflictoArr);

    -- Si hay conflictos y no se confirmó, retornar advertencia sin modificar datos
    IF v_Count > 0 AND NOT COALESCE(_Confirmar, FALSE) THEN
        _Exitoso    := 3;
        _Conflictos := v_ConflictoArr::TEXT;
        _Mensaje    := 'El operador tiene ' || v_Count ||
                       ' reserva(s) ASIGNADA(s) en horarios que se intentan desactivar. ' ||
                       'Si continúa, dichas reservas volverán a estado RESERVADO y ' ||
                       'se liberará el operador y la grúa asignada. ¿Desea continuar?';
        RETURN;
    END IF;

    -- Liberar reservas conflictivas si se confirmó
    IF v_Count > 0 THEN
        UPDATE "Reserva"
        SET    "EstadoOperacion"    = 'RESERVADO',
               "IdOperador"         = NULL,
               "IdGrua"             = NULL,
               "FechaActualizacion" = NOW(),
               "ActualizadoPor"     = _ActualizadoPor
        WHERE  "IdOperador"      = _IdOperador
          AND  "EstadoOperacion" = 'ASIGNADO'
          AND  "Estado"          = 'ACTIVO'
          AND  NOT EXISTS (
              SELECT 1
              FROM   jsonb_array_elements(v_DispJson) jd
              WHERE  (jd->>'NroDia')::SMALLINT =
                         (EXTRACT(DOW FROM "Reserva"."FechaServicio")::INT + 1)::SMALLINT
                AND  (jd->>'HoraInicio')::TIME <= "Reserva"."HoraInicio"
                AND  (jd->>'HoraFin')::TIME    >= "Reserva"."HoraFin"
          );
    END IF;

    -- Desactivar disponibilidad actual
    UPDATE "Disponibilidad"
    SET    "Estado"             = 'INACTIVO',
           "FechaActualizacion" = NOW(),
           "ActualizadoPor"     = _ActualizadoPor
    WHERE  "IdOperador" = _IdOperador
      AND  "Estado"     = 'ACTIVO';

    -- Insertar nueva disponibilidad
    INSERT INTO "Disponibilidad" (
        "NroDia", "NombreDia", "Estado", "IdOperador",
        "FechaCreacion", "CreadoPor", "HoraInicio", "HoraFin"
    )
    SELECT
        (jd->>'NroDia')::SMALLINT,
        jd->>'NombreDia',
        'ACTIVO',
        _IdOperador,
        NOW(),
        _ActualizadoPor,
        (jd->>'HoraInicio')::TIME,
        (jd->>'HoraFin')::TIME
    FROM   jsonb_array_elements(v_DispJson) AS jd;

    COMMIT;
    _Exitoso    := 1;
    _Mensaje    := 'Disponibilidad actualizada correctamente.';
    _Conflictos := '[]';
END;
$$;
