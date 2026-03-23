--ÍNDICES PARA EL MÓDULO DE RESERVA

--dbo.Reserva
-- FechaServicio + CantidadCarga + EstadoOperacion, con HoraInicio/HoraFin en el rango
CREATE INDEX IX_Reserva_Fecha_Carga_Estado
ON dbo.Reserva (FechaServicio, CantidadCarga, EstadoOperacion)
INCLUDE (HoraInicio, HoraFin);

--dbo.TimerReserva
-- Consulta por fecha + capacidad (sp_ListHorarios y sp_ValidarHorario)
CREATE INDEX IX_TimerReserva_Fecha_Carga
ON dbo.TimerReserva (FechaServicio, CantidadCarga)
INCLUDE (HoraInicio, HoraFin, TimerExpiracion, FechaCreacion);

-- DELETE de timers vencidos (se ejecuta en cada llamada a sp_ValidarHorario)
-- Columna calculada persistida para indexar directamente la fecha de expiración
--ALTER TABLE dbo.TimerReserva
--ADD FechaExpiracion AS DATEADD(MINUTE, TimerExpiracion, FechaCreacion) PERSISTED;

--CREATE INDEX IX_TimerReserva_Expiracion
--ON dbo.TimerReserva (FechaExpiracion);

--dbo.Grua
-- COUNT(*) con GROUP BY Capacidad filtrado por Estado + EstadoOperacion
CREATE INDEX IX_Grua_Estado_Operacion_Capacidad
ON dbo.Grua (Estado, EstadoOperacion, Capacidad);


--dbo.Excepcion
-- Filtro por Fecha + Estado, necesita TiempoInicio/TiempoFinal sin key lookup
CREATE INDEX IX_Excepcion_Fecha_Estado
ON dbo.Excepcion (Fecha, Estado)
INCLUDE (TiempoInicio, TiempoFinal);


--dbo.HorarioRegular
-- sp_ListHorariosDisponibles filtra por DiaSemana + Estado
CREATE INDEX IX_HorarioRegular_Dia_Estado
ON dbo.HorarioRegular (DiaSemana, Estado)
INCLUDE (HoraInicio, HoraFin);


------------------------------------------------------------------------------------------------------------------------------