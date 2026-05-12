-- ============================================================
-- ACTUALIZACIÓN DE HORARIOS REGULARES
-- ============================================================

-- ── sp_UpdConfiguracionHorario ─────────────────────────────────
-- Actualiza uno o más registros de ConfiguracionHorario en una sola llamada.
-- Diseñado para el botón "Guardar Horarios": el frontend envía todos los
-- registros modificados juntos tras confirmar los cambios.
--
-- Parámetros de entrada:
--   _Horarios       → JSON array con los registros a actualizar:
--                     [{"Id":7,"Estado":"ACTIVO","HoraInicio":"08:00","HoraFinal":"18:00"}, ...]
--   _ActualizadoPor → ID del usuario que realiza la operación
--
-- Valores de salida _Exitoso:
--   0 → Error de validación (ver _Mensaje); ningún registro fue modificado
--   1 → Éxito; todos los registros fueron actualizados

DROP PROCEDURE IF EXISTS sp_UpdConfiguracionHorario(TEXT, INT, INT, TEXT);
CREATE OR REPLACE PROCEDURE sp_UpdConfiguracionHorario(
    _Horarios       TEXT,
    _ActualizadoPor INT,
    INOUT _Exitoso  INT,
    INOUT _Mensaje  TEXT
)
LANGUAGE plpgsql AS $$
DECLARE
    v_HorariosJson JSONB;
BEGIN
    _Exitoso := 0;
    _Mensaje  := '';

    v_HorariosJson := _Horarios::JSONB;

    IF jsonb_array_length(v_HorariosJson) = 0 THEN
        _Mensaje := 'Debe enviar al menos un horario para actualizar.';
        RETURN;
    END IF;

    -- Verificar que todos los Ids existen
    IF EXISTS (
        SELECT 1
        FROM   jsonb_array_elements(v_HorariosJson) jh
        WHERE  NOT EXISTS (
            SELECT 1 FROM "ConfiguracionHorario" ch
            WHERE  ch."Id" = (jh->>'Id')::INT
        )
    ) THEN
        _Mensaje := 'Uno o más horarios enviados no fueron encontrados.';
        RETURN;
    END IF;

    -- Verificar que HoraInicio < HoraFinal en todos los registros
    IF EXISTS (
        SELECT 1
        FROM   jsonb_array_elements(v_HorariosJson) jh
        WHERE  (jh->>'HoraInicio')::TIME >= (jh->>'HoraFinal')::TIME
    ) THEN
        _Mensaje := 'La hora de inicio debe ser menor a la hora final en todos los registros.';
        RETURN;
    END IF;

    -- Actualizar todos los registros en una sola operación
    UPDATE "ConfiguracionHorario" ch
    SET    "Estado"              = (jh->>'Estado')::VARCHAR(10),
           "HoraInicio"         = (jh->>'HoraInicio')::TIME,
           "HoraFinal"          = (jh->>'HoraFinal')::TIME,
           "FechaActualizacion" = NOW(),
           "ActualizadoPor"     = _ActualizadoPor
    FROM   jsonb_array_elements(v_HorariosJson) jh
    WHERE  ch."Id" = (jh->>'Id')::INT;

    COMMIT;
    _Exitoso := 1;
    _Mensaje  := 'Horarios actualizados correctamente.';
END;
$$;
