-- ============================================================
-- CONFIGURACIÓN DE HORARIOS REGULARES
-- ============================================================

-- ── fn_ListConfiguracionHorario ──────────────────────────────
-- Retorna la configuración de horarios regulares filtrada por Rol y Estado.
-- Ordenado de Lunes a Domingo (Domingo = NroDia 1 → posición final).
--
-- Parámetros:
--   _Rol    → 'CLIENTE' | 'ADMINISTRADOR' | 'STAFF' | 'OPERADOR' | NULL (todos)
--   _Estado → 'ACTIVO' | 'INACTIVO' | NULL (todos)

DROP FUNCTION IF EXISTS fn_ListConfiguracionHorario(VARCHAR, VARCHAR);
CREATE OR REPLACE FUNCTION fn_ListConfiguracionHorario(
    _Rol    VARCHAR(20),
    _Estado VARCHAR(10)
)
RETURNS TABLE(
    "Id"         INT,
    "NroDia"     SMALLINT,
    "NombreDia"  VARCHAR(9),
    "Estado"     VARCHAR(10),
    "HoraInicio" TIME(6),
    "HoraFinal"  TIME(6)
)
LANGUAGE plpgsql AS $$
BEGIN
    RETURN QUERY
    SELECT
        ch."Id"::INT,
        ch."NroDia",
        ch."NombreDia",
        ch."Estado",
        ch."HoraInicio",
        ch."HoraFinal"
    FROM   "ConfiguracionHorario" ch
    WHERE  (_Rol    IS NULL OR ch."Rol"    = _Rol)
      AND  (_Estado IS NULL OR ch."Estado" = _Estado)
    ORDER  BY CASE WHEN ch."NroDia" = 1 THEN 8 ELSE ch."NroDia" END;
END;
$$;
