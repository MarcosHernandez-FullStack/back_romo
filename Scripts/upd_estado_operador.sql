-- ── sp_UpdEstadoOperador ─────────────────────────────────────
-- Cambia el Estado de un operador (ACTIVO ↔ INACTIVO).
-- Actualiza tanto "Operador" como "Usuario" para sincronizar
-- el acceso a la app móvil con el estado en el sistema.
--
-- Parámetros:
--   _IdOperador     → ID de la tabla Operador
--   _NuevoEstado    → 'ACTIVO' | 'INACTIVO'
--   _ActualizadoPor → ID del usuario que realiza la acción
--
-- _Exitoso: 0=error, 1=éxito

DROP PROCEDURE IF EXISTS sp_UpdEstadoOperador(INT, VARCHAR, INT, INT, TEXT);
CREATE OR REPLACE PROCEDURE sp_UpdEstadoOperador(
    _IdOperador     INT,
    _NuevoEstado    VARCHAR(10),
    _ActualizadoPor INT,
    INOUT _Exitoso  INT,
    INOUT _Mensaje  TEXT
)
LANGUAGE plpgsql AS $$
DECLARE
    v_IdUsuario INT;
BEGIN
    _Exitoso := 0;
    _Mensaje  := '';

    -- Verificar que el operador existe y obtener su IdUsuario
    SELECT "IdUsuario" INTO v_IdUsuario
    FROM   "Operador"
    WHERE  "Id" = _IdOperador;

    IF NOT FOUND THEN
        _Mensaje := 'El operador no existe.';
        RETURN;
    END IF;

    -- Verificar que no se intenta asignar el mismo estado actual
    IF EXISTS (
        SELECT 1 FROM "Operador"
        WHERE  "Id"     = _IdOperador
          AND  "Estado" = _NuevoEstado
    ) THEN
        _Mensaje := 'El operador ya se encuentra en estado ' || _NuevoEstado || '.';
        RETURN;
    END IF;

    -- Actualizar estado en Operador
    UPDATE "Operador"
    SET    "Estado"             = _NuevoEstado,
           "FechaActualizacion" = NOW(),
           "ActualizadoPor"     = _ActualizadoPor
    WHERE  "Id" = _IdOperador;

    -- Actualizar estado en Usuario (controla acceso a app móvil)
    UPDATE "Usuario"
    SET    "Estado"             = _NuevoEstado,
           "FechaActualizacion" = NOW(),
           "ActualizadoPor"     = _ActualizadoPor
    WHERE  "Id" = v_IdUsuario;

    COMMIT;
    _Exitoso := 1;
    _Mensaje  := CASE _NuevoEstado
        WHEN 'INACTIVO' THEN 'Operador desactivado correctamente.'
        WHEN 'ACTIVO'   THEN 'Operador reactivado correctamente.'
        ELSE                 'Estado actualizado correctamente.'
    END;
END;
$$;
