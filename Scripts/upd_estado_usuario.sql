-- ── sp_UpdEstadoUsuario ─────────────────────────────────────
-- Cambia el Estado de un usuario admin/staff (ACTIVO ↔ INACTIVO).
--
-- Parámetros:
--   _IdUsuario      → ID del usuario a modificar
--   _NuevoEstado    → 'ACTIVO' | 'INACTIVO'
--   _ActualizadoPor → ID del usuario que realiza la acción
--
-- _Exitoso: 0=error, 1=éxito

DROP PROCEDURE IF EXISTS sp_UpdEstadoUsuario(INT, VARCHAR, INT, INT, TEXT);
CREATE OR REPLACE PROCEDURE sp_UpdEstadoUsuario(
    _IdUsuario      INT,
    _NuevoEstado    VARCHAR(10),
    _ActualizadoPor INT,
    INOUT _Exitoso  INT,
    INOUT _Mensaje  TEXT
)
LANGUAGE plpgsql AS $$
BEGIN
    _Exitoso := 0;
    _Mensaje  := '';

    IF NOT EXISTS (SELECT 1 FROM "Usuario" WHERE "Id" = _IdUsuario) THEN
        _Mensaje := 'El usuario no existe.';
        RETURN;
    END IF;

    IF EXISTS (
        SELECT 1 FROM "Usuario"
        WHERE  "Id"     = _IdUsuario
          AND  "Estado" = _NuevoEstado
    ) THEN
        _Mensaje := 'El usuario ya se encuentra en estado ' || _NuevoEstado || '.';
        RETURN;
    END IF;

    UPDATE "Usuario"
    SET    "Estado"             = _NuevoEstado,
           "FechaActualizacion" = NOW(),
           "ActualizadoPor"     = _ActualizadoPor
    WHERE  "Id" = _IdUsuario;

    COMMIT;
    _Exitoso := 1;
    _Mensaje  := CASE _NuevoEstado
        WHEN 'INACTIVO' THEN 'Usuario dado de baja correctamente.'
        WHEN 'ACTIVO'   THEN 'Usuario reactivado correctamente.'
        ELSE                 'Estado actualizado correctamente.'
    END;
END;
$$;
