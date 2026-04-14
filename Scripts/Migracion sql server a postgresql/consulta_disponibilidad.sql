
-- ============================================================
-- Reporte de ocupación por bloque horario y capacidad de grúa
-- Parámetro: reemplaza '2026-12-07' con la fecha deseada
-- ============================================================
WITH

"Bloques" AS (
    SELECT make_time(n, 0, 0) AS "Bloque"
    FROM generate_series(8, 23) AS s(n)
),

"Capacidades" AS (
    SELECT DISTINCT "Capacidad"
    FROM "Grua"
    WHERE "Estado"          = 'ACTIVO'
      AND "EstadoOperacion" = 'OPERATIVA'
),

-- Total de grúas capaces por grupo: cuenta todas con Capacidad >= grupo
"GruasPorCapacidad" AS (
    SELECT
        c."Capacidad",
        (
            SELECT COUNT(*)::INT
            FROM   "Grua" g
            WHERE  g."Estado"          = 'ACTIVO'
              AND  g."EstadoOperacion" = 'OPERATIVA'
              AND  g."Capacidad"       >= c."Capacidad"
        ) AS "TotalGruas"
    FROM "Capacidades" c
),

"ReservasActivas" AS (
    SELECT "HoraInicio", "HoraFin", "CantidadCarga"
    FROM   "Reserva"
    WHERE  "FechaServicio"  = '2026-12-10'
      AND  "Estado"          = 'ACTIVO'
      AND  "EstadoOperacion" != 'CANCELADO'
    UNION ALL
    SELECT "HoraInicio", "HoraFin", "CantidadCarga"
    FROM   "TimerReserva"
    WHERE  "FechaServicio"  = '2026-12-10'
      AND  "Estado"          = 'ACTIVO'
      AND  "EstadoOperacion" != 'CANCELADO'
),

"Base" AS (
    SELECT b."Bloque", c."Capacidad"
    FROM   "Bloques"     b
    CROSS JOIN "Capacidades" c
),

-- Reservas que ocupan cada bloque con CantidadCarga = Capacidad exacta
"Ocupacion" AS (
    SELECT
        bs."Bloque",
        bs."Capacidad",
        COUNT(r."CantidadCarga")::INT AS "CantidadReservas"
    FROM "Base" bs
    LEFT JOIN "ReservasActivas" r
           ON r."CantidadCarga" = bs."Capacidad"
          AND (
                CASE
                    WHEN r."HoraFin" > r."HoraInicio"
                        THEN bs."Bloque" >= r."HoraInicio"
                         AND bs."Bloque" <  r."HoraFin"
                    ELSE bs."Bloque" >= r."HoraInicio"
                      OR bs."Bloque" <  r."HoraFin"
                END
              )
    GROUP BY bs."Bloque", bs."Capacidad"
)

SELECT
    o."Bloque"                                          AS "Hora",
    o."Capacidad",
    o."CantidadReservas",
    g."TotalGruas"                                      AS "CantidadGruas",
    GREATEST(g."TotalGruas" - o."CantidadReservas", 0) AS "CantidadGruasDisponible"
FROM "Ocupacion"         o
JOIN "GruasPorCapacidad" g ON g."Capacidad" = o."Capacidad"
ORDER BY o."Bloque", o."Capacidad";
