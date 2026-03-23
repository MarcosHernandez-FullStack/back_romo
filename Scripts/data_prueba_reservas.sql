USE [db_romo];
GO

-- ================================================================
-- DATOS DE PRUEBA - SISTEMA DE RESERVAS
-- Período  : 23 Marzo – 19 Abril 2026 (Lunes a Sábado, sin domingos)
-- Capacidad : Cap≥1 → 55 grúas | Cap≥2 → 43 grúas | Cap≥3 → 11 grúas
-- Horario   : 08:00 – 23:00 (ADMINISTRADOR)
-- Cliente   : Id=1 | CreadoPor: 803
-- ================================================================

-- Descomentar para limpiar data previa del período:
-- DELETE FROM dbo.Reserva WHERE FechaServicio BETWEEN '2026-03-23' AND '2026-04-19';

-- ================================================================
-- SEMANA 1: BAJA DEMANDA (23-28 Marzo)
-- Operación normal, 8 reservas/día en slots variados.
-- Ningún slot llega al límite → todos siguen visibles como libres.
-- ================================================================
INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
VALUES
-- Lun 23/03
(1,1,'Av. Próceres 120, Lima','-12.0464','-77.0428','Parque Ind. VES','-12.1950','-76.9420',22.50,55,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','08:00:00','10:00:00','2026-03-23',NULL,NULL,'2026-03-21 09:00:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Jr. Huallaga 300, Lima','-12.0510','-77.0320','Ate Vitarte Centro','-12.0260','-76.9210',18.20,45,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','10:00:00','12:00:00','2026-03-23',NULL,NULL,'2026-03-21 09:10:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Argentina 1500, Lima','-12.0540','-77.0850','Lurín Km 40','-12.2800','-76.8700',35.80,90,60,80,3,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','12:00:00','15:00:00','2026-03-23',NULL,NULL,'2026-03-21 09:20:00',NULL,803,NULL,'DISPONIBLE'),
(2,1,'Av. Colonial 800, Lima','-12.0580','-77.0780','SJM Centro','-12.1550','-76.9720',15.40,38,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','14:00:00','16:00:00','2026-03-23',NULL,NULL,'2026-03-21 09:30:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Calle Huáscar 45, Lima','-12.0630','-77.0410','Los Olivos Norte','-11.9950','-77.0730',10.20,25,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','16:00:00','17:00:00','2026-03-23',NULL,NULL,'2026-03-21 09:40:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Benavides 3200, Lima','-12.1180','-77.0010','Chorrillos Industrial','-12.1630','-77.0210',8.50,20,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','17:00:00','18:00:00','2026-03-23',NULL,NULL,'2026-03-21 09:50:00',NULL,803,NULL,'DISPONIBLE'),
(2,1,'Jr. Lampa 560, Lima','-12.0480','-77.0330','Santa Anita Industrial','-12.0420','-76.9650',14.70,36,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','18:00:00','20:00:00','2026-03-23',NULL,NULL,'2026-03-21 10:00:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Túpac Amaru 2100','-11.9850','-77.0520','Independencia Centro','-11.9930','-77.0570',5.30,13,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','20:00:00','21:00:00','2026-03-23',NULL,NULL,'2026-03-21 10:10:00',NULL,803,NULL,'DISPONIBLE'),
-- Mar 24/03
(1,1,'Av. La Marina 2000, Lima','-12.0790','-77.0960','Callao Puerto','-12.0620','-77.1460',12.30,30,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','08:00:00','09:00:00','2026-03-24',NULL,NULL,'2026-03-22 09:00:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Arequipa 3800, Lima','-12.1030','-77.0320','Miraflores Costa','-12.1220','-77.0270',6.80,17,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','09:00:00','10:00:00','2026-03-24',NULL,NULL,'2026-03-22 09:10:00',NULL,803,NULL,'DISPONIBLE'),
(2,1,'Av. Universitaria 5400','-11.9560','-77.0590','Comas Industrial','-11.9420','-77.0480',9.40,23,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','11:00:00','12:00:00','2026-03-24',NULL,NULL,'2026-03-22 09:20:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Jr. De La Unión 700, Lima','-12.0450','-77.0310','Villa María del Triunfo','-12.1730','-76.9530',19.80,50,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','13:00:00','15:00:00','2026-03-24',NULL,NULL,'2026-03-22 09:30:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Brasil 1200, Lima','-12.0750','-77.0630','La Victoria Industrial','-12.0700','-77.0030',14.20,35,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','15:00:00','17:00:00','2026-03-24',NULL,NULL,'2026-03-22 09:40:00',NULL,803,NULL,'DISPONIBLE'),
(3,1,'Av. Javier Prado 4200','-12.0950','-77.0030','Ate Zona Industrial','-12.0380','-76.9440',16.50,41,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','17:00:00','19:00:00','2026-03-24',NULL,NULL,'2026-03-22 09:50:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Abancay 1100, Lima','-12.0500','-77.0240','SJL Zona Este','-11.9870','-76.9850',21.30,53,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','19:00:00','21:00:00','2026-03-24',NULL,NULL,'2026-03-22 10:00:00',NULL,803,NULL,'DISPONIBLE'),
(2,1,'Av. Grau 900, Lima','-12.0560','-77.0180','El Agustino','-12.0380','-76.9980',9.10,23,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','21:00:00','22:00:00','2026-03-24',NULL,NULL,'2026-03-22 10:10:00',NULL,803,NULL,'DISPONIBLE'),
-- Mie 25/03
(1,1,'Av. Venezuela 3400, Lima','-12.0610','-77.0840','Callao Zona Franca','-12.0550','-77.1200',10.80,27,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','08:00:00','09:00:00','2026-03-25',NULL,NULL,'2026-03-23 09:00:00',NULL,803,NULL,'DISPONIBLE'),
(2,1,'Jr. Ancash 800, Lima','-12.0460','-77.0280','Huachipa Industrial','-11.9980','-76.9150',24.60,62,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','10:00:00','12:00:00','2026-03-25',NULL,NULL,'2026-03-23 09:10:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Tacna 1200, Lima','-12.0470','-77.0410','Puente Piedra','-11.8660','-77.0740',30.40,76,60,80,3,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','12:00:00','15:00:00','2026-03-25',NULL,NULL,'2026-03-23 09:20:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Calle Camaná 560, Lima','-12.0480','-77.0330','Surquillo Comercial','-12.1110','-77.0090',11.20,28,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','15:00:00','16:00:00','2026-03-25',NULL,NULL,'2026-03-23 09:30:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Salaverry 2800, Lima','-12.0890','-77.0520','Lince Industrial','-12.0840','-77.0360',5.80,14,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','17:00:00','18:00:00','2026-03-25',NULL,NULL,'2026-03-23 09:40:00',NULL,803,NULL,'DISPONIBLE'),
(2,1,'Av. Petit Thouars 3100','-12.0970','-77.0410','San Borja Norte','-12.1040','-77.0010',12.40,31,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','19:00:00','20:00:00','2026-03-25',NULL,NULL,'2026-03-23 09:50:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Jr. Ica 300, Lima','-12.0490','-77.0280','Barranco Puerto','-12.1480','-77.0230',13.60,34,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','20:00:00','22:00:00','2026-03-25',NULL,NULL,'2026-03-23 10:00:00',NULL,803,NULL,'DISPONIBLE'),
(3,1,'Av. Paseo Colón 700, Lima','-12.0560','-77.0440','Cercado Norte','-12.0410','-77.0380',6.20,15,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','22:00:00','23:00:00','2026-03-25',NULL,NULL,'2026-03-23 10:10:00',NULL,803,NULL,'DISPONIBLE'),
-- Jue 26/03
(1,1,'Av. Grau 1800, Lima','-12.0550','-77.0160','El Agustino Industrial','-12.0380','-76.9980',9.60,24,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','08:00:00','09:00:00','2026-03-26',NULL,NULL,'2026-03-24 09:00:00',NULL,803,NULL,'DISPONIBLE'),
(3,1,'Av. Iquitos 1200, Lima','-12.0690','-77.0210','Lurigancho Industrial','-11.9830','-76.9000',28.70,72,60,80,3,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','09:00:00','12:00:00','2026-03-26',NULL,NULL,'2026-03-24 09:10:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Paseo República 3600','-12.1100','-77.0180','Surco Comercial','-12.1350','-76.9930',10.20,25,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','13:00:00','14:00:00','2026-03-26',NULL,NULL,'2026-03-24 09:20:00',NULL,803,NULL,'DISPONIBLE'),
(2,1,'Calle Schell 400, Lima','-12.1210','-77.0310','Miraflores Hoteles','-12.1250','-77.0300',4.20,10,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','15:00:00','16:00:00','2026-03-26',NULL,NULL,'2026-03-24 09:30:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Angamos 2400, Lima','-12.1180','-77.0100','Villa María Industrial','-12.1760','-76.9520',18.90,47,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','16:00:00','18:00:00','2026-03-26',NULL,NULL,'2026-03-24 09:40:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Jr. Quilca 200, Lima','-12.0460','-77.0350','Rímac Industrial','-12.0290','-77.0280',7.10,18,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','20:00:00','21:00:00','2026-03-26',NULL,NULL,'2026-03-24 09:50:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Bolívar 2200, Lima','-12.0730','-77.0880','Pueblo Libre Centro','-12.0760','-77.0810',3.80,9,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','21:00:00','22:00:00','2026-03-26',NULL,NULL,'2026-03-24 10:00:00',NULL,803,NULL,'DISPONIBLE'),
(2,1,'Av. Brasil 3100, Lima','-12.0790','-77.0730','Magdalena del Mar','-12.0940','-77.0650',7.60,19,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','22:00:00','23:00:00','2026-03-26',NULL,NULL,'2026-03-24 10:10:00',NULL,803,NULL,'DISPONIBLE'),
-- Vie 27/03
(2,1,'Av. Canadá 1800, Lima','-12.0850','-77.0030','San Isidro Financiero','-12.0990','-77.0370',8.40,21,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','08:00:00','09:00:00','2026-03-27',NULL,NULL,'2026-03-25 09:00:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Calle Los Álamos 300, Lima','-12.0920','-77.0070','San Luis Industrial','-12.0790','-76.9980',6.20,15,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','10:00:00','11:00:00','2026-03-27',NULL,NULL,'2026-03-25 09:10:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Defensores del Morro 400','-12.1740','-77.0110','Chorrillos Zona Pesca','-12.1840','-77.0190',4.50,11,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','11:00:00','12:00:00','2026-03-27',NULL,NULL,'2026-03-25 09:20:00',NULL,803,NULL,'DISPONIBLE'),
(3,1,'Av. La Fontana 1200','-12.0800','-76.9740','La Molina Industrial','-12.0880','-76.9400',12.80,32,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','13:00:00','14:00:00','2026-03-27',NULL,NULL,'2026-03-25 09:30:00',NULL,803,NULL,'DISPONIBLE'),
(2,1,'Av. Pachacútec 2600, Lima','-12.1790','-76.9740','VES Industrial Sur','-12.2020','-76.9460',16.30,41,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','15:00:00','17:00:00','2026-03-27',NULL,NULL,'2026-03-25 09:40:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Tomás Marsano 3400','-12.1400','-76.9930','Surco Norte','-12.1280','-76.9970',7.20,18,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','17:00:00','18:00:00','2026-03-27',NULL,NULL,'2026-03-25 09:50:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Jr. Azángaro 900, Lima','-12.0470','-77.0260','Cercado Industrial','-12.0530','-77.0370',5.10,13,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','19:00:00','20:00:00','2026-03-27',NULL,NULL,'2026-03-25 10:00:00',NULL,803,NULL,'DISPONIBLE'),
(2,1,'Av. Del Ejército 1100','-12.1080','-77.0590','Magdalena del Mar','-12.0940','-77.0650',4.80,12,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','21:00:00','22:00:00','2026-03-27',NULL,NULL,'2026-03-25 10:10:00',NULL,803,NULL,'DISPONIBLE'),
-- Sab 28/03
(1,1,'Av. Colonial 2400, Lima','-12.0580','-77.1060','Callao Industrial','-12.0520','-77.1220',8.90,22,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','08:00:00','09:00:00','2026-03-28',NULL,NULL,'2026-03-26 09:00:00',NULL,803,NULL,'DISPONIBLE'),
(2,1,'Jr. Paruro 400, Lima','-12.0490','-77.0220','Zárate Industrial','-11.9830','-77.0010',17.40,44,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','09:00:00','11:00:00','2026-03-28',NULL,NULL,'2026-03-26 09:10:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Naranjal 800, Lima','-11.9780','-77.0600','Los Olivos Centro','-11.9970','-77.0680',6.50,16,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','11:00:00','12:00:00','2026-03-28',NULL,NULL,'2026-03-26 09:20:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Alfredo Mendiola 3200','-11.9600','-77.0570','Comas Centro','-11.9430','-77.0480',10.10,25,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','12:00:00','13:00:00','2026-03-28',NULL,NULL,'2026-03-26 09:30:00',NULL,803,NULL,'DISPONIBLE'),
(3,1,'Av. Primavera 1800, Lima','-12.0960','-76.9890','La Molina Sur','-12.0930','-76.9490',14.60,37,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','13:00:00','15:00:00','2026-03-28',NULL,NULL,'2026-03-26 09:40:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Calle Monte Rosa 200','-12.1050','-76.9660','Surco Residencial','-12.1300','-76.9900',10.80,27,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','15:00:00','16:00:00','2026-03-28',NULL,NULL,'2026-03-26 09:50:00',NULL,803,NULL,'DISPONIBLE'),
(2,1,'Av. Benavides 5200','-12.1320','-76.9730','Monterrico Norte','-12.0990','-76.9660',11.40,28,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','16:00:00','17:00:00','2026-03-28',NULL,NULL,'2026-03-26 10:00:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Próceres 2800','-12.0200','-76.9780','SJL Industrial','-11.9920','-76.9820',13.20,33,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','17:00:00','19:00:00','2026-03-28',NULL,NULL,'2026-03-26 10:10:00',NULL,803,NULL,'DISPONIBLE');
GO

-- ================================================================
-- SEMANA 2: ALTA DEMANDA (30-31 Marzo, 1-4 Abril)
-- Lun 30/03: slot 09:00 completamente lleno cap.1 (55 reservas)
-- Resultado esperado: slot 09:00 NO aparece en listado
-- ================================================================
-- ================================================================
-- Lun 30/03: LÍMITE CAP.2 - 3 slots consecutivos con 42 pre-cargadas
-- Objetivo: agregar 1 reserva cap.2 → pasa de 42 → 43 y se bloquea
-- ================================================================
INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 42
    2, 1,
    'Av. LímCap2 11h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. DestCap2 11h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    10.00 + (n % 15),
    25 + (n % 20), 60, 80, 1,
    5.00, 25.00, 'RESERVADO', 'ACTIVO', 'PENDIENTE',
    '11:00:00', '12:00:00', '2026-03-30',
    NULL, NULL, '2026-03-28 07:00:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO

INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 42
    2, 1,
    'Av. LímCap2 12h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. DestCap2 12h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    11.00 + (n % 15),
    27 + (n % 20), 60, 80, 1,
    5.00, 25.00, 'RESERVADO', 'ACTIVO', 'PENDIENTE',
    '12:00:00', '13:00:00', '2026-03-30',
    NULL, NULL, '2026-03-28 07:30:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO

INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 42
    2, 1,
    'Av. LímCap2 13h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. DestCap2 13h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    12.00 + (n % 15),
    29 + (n % 20), 60, 80, 1,
    5.00, 25.00, 'RESERVADO', 'ACTIVO', 'PENDIENTE',
    '13:00:00', '14:00:00', '2026-03-30',
    NULL, NULL, '2026-03-28 08:00:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO

-- ================================================================
-- Mar 31/03: LÍMITE CAP.2 - 3 slots consecutivos con 41 pre-cargadas
-- Objetivo: agregar 1 → 42 CRÍTICO | agregar 2 → 43 BLOQUEADO
-- ================================================================
INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 41
    2, 1,
    'Av. CritCap2 11h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. DestCrit2 11h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    10.00 + (n % 15),
    25 + (n % 20), 60, 80, 1,
    5.00, 25.00, 'RESERVADO', 'ACTIVO', 'PENDIENTE',
    '11:00:00', '12:00:00', '2026-03-31',
    NULL, NULL, '2026-03-29 07:00:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO

INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 41
    2, 1,
    'Av. CritCap2 12h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. DestCrit2 12h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    11.00 + (n % 15),
    27 + (n % 20), 60, 80, 1,
    5.00, 25.00, 'RESERVADO', 'ACTIVO', 'PENDIENTE',
    '12:00:00', '13:00:00', '2026-03-31',
    NULL, NULL, '2026-03-29 07:30:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO

INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 41
    2, 1,
    'Av. CritCap2 13h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. DestCrit2 13h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    12.00 + (n % 15),
    29 + (n % 20), 60, 80, 1,
    5.00, 25.00, 'RESERVADO', 'ACTIVO', 'PENDIENTE',
    '13:00:00', '14:00:00', '2026-03-31',
    NULL, NULL, '2026-03-29 08:00:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO


INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 55
    1, 1,
    'Av. Origen Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. Destino Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    10.50 + (n % 15),
    26 + (n % 25), 60, 80, 1,
    5.00, 25.00, 'RESERVADO', 'ACTIVO', 'PENDIENTE',
    '09:00:00', '10:00:00', '2026-03-30',
    NULL, NULL, '2026-03-28 08:00:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO

-- Lun 30/03: otros slots normales (3 reservas adicionales en distintos horarios)
INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
VALUES
(1,1,'Av. La Marina 1800','-12.0790','-77.0960','Callao Norte','-12.0500','-77.1400',11.50,29,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','14:00:00','15:00:00','2026-03-30',NULL,NULL,'2026-03-28 09:00:00',NULL,803,NULL,'DISPONIBLE'),
(2,1,'Av. Argentina 2200','-12.0540','-77.0920','Ventanilla Industrial','-11.8900','-77.1320',28.60,72,60,80,3,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','16:00:00','19:00:00','2026-03-30',NULL,NULL,'2026-03-28 09:10:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Calle Carabaya 800','-12.0470','-77.0300','Santa Anita Este','-12.0350','-76.9640',16.80,42,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','20:00:00','22:00:00','2026-03-30',NULL,NULL,'2026-03-28 09:20:00',NULL,803,NULL,'DISPONIBLE');
GO

-- ================================================================
-- Mar 31/03: slot 14:00 lleno para cap.2 (43 reservas, NroBloques=2)
-- Resultado: slots 14:00 y 15:00 bloqueados para cap.2 y cap.3
--            pero libres para cap.1 (55-43=12 grúas cap.1 DISPONIBLEs)
-- ================================================================
INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 43
    2, 1,
    'Av. Origen Cap2 Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. Destino Cap2 Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    12.00 + (n % 20),
    30 + (n % 30), 60, 80, 2,
    5.00, 25.00, 'RESERVADO', 'ACTIVO', 'PENDIENTE',
    '14:00:00', '16:00:00', '2026-03-31',
    NULL, NULL, '2026-03-29 08:00:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO

-- ================================================================
-- Mie 1/04: LÍMITE CAP.3 - 3 slots consecutivos con 10 pre-cargadas
-- Objetivo: agregar 1 reserva cap.3 → pasa de 10 → 11 y se bloquea
-- ================================================================
INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 10
    3, 1,
    'Av. LímCap3 08h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. DestCap3 08h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    15.00 + (n % 15),
    37 + (n % 20), 60, 80, 1,
    5.00, 25.00, 'RESERVADO', 'ACTIVO', 'PENDIENTE',
    '08:00:00', '09:00:00', '2026-04-01',
    NULL, NULL, '2026-03-30 07:00:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO

INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 10
    3, 1,
    'Av. LímCap3 09h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. DestCap3 09h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    16.00 + (n % 15),
    39 + (n % 20), 60, 80, 1,
    5.00, 25.00, 'RESERVADO', 'ACTIVO', 'PENDIENTE',
    '09:00:00', '10:00:00', '2026-04-01',
    NULL, NULL, '2026-03-30 07:30:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO

INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 10
    3, 1,
    'Av. LímCap3 10h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. DestCap3 10h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    17.00 + (n % 15),
    41 + (n % 20), 60, 80, 1,
    5.00, 25.00, 'RESERVADO', 'ACTIVO', 'PENDIENTE',
    '10:00:00', '11:00:00', '2026-04-01',
    NULL, NULL, '2026-03-30 08:00:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO

-- ================================================================
-- Jue 2/04: LÍMITE CAP.3 - 3 slots consecutivos con 9 pre-cargadas
-- Objetivo: agregar 1 → 10 CRÍTICO | agregar 2 → 11 BLOQUEADO
-- ================================================================
INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 9
    3, 1,
    'Av. CritCap3 08h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. DestCrit3 08h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    15.00 + (n % 15),
    37 + (n % 20), 60, 80, 1,
    5.00, 25.00, 'RESERVADO', 'ACTIVO', 'PENDIENTE',
    '08:00:00', '09:00:00', '2026-04-02',
    NULL, NULL, '2026-03-31 07:00:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO

INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 9
    3, 1,
    'Av. CritCap3 09h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. DestCrit3 09h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    16.00 + (n % 15),
    39 + (n % 20), 60, 80, 1,
    5.00, 25.00, 'RESERVADO', 'ACTIVO', 'PENDIENTE',
    '09:00:00', '10:00:00', '2026-04-02',
    NULL, NULL, '2026-03-31 07:30:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO

INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 9
    3, 1,
    'Av. CritCap3 10h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. DestCrit3 10h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    17.00 + (n % 15),
    41 + (n % 20), 60, 80, 1,
    5.00, 25.00, 'RESERVADO', 'ACTIVO', 'PENDIENTE',
    '10:00:00', '11:00:00', '2026-04-02',
    NULL, NULL, '2026-03-31 08:00:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO

-- ================================================================
-- Mie 1/04: slot 10:00 parcialmente ocupado (30 de 55)
-- Resultado: slot visible pero con baja disponibilidad
-- ================================================================
INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 30
    1, 1,
    'Av. Origen Parcial ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. Destino Parcial ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    8.00 + (n % 12),
    20 + (n % 20), 60, 80, 1,
    5.00, 25.00, 'RESERVADO', 'ACTIVO', 'PENDIENTE',
    '10:00:00', '11:00:00', '2026-04-01',
    NULL, NULL, '2026-03-30 08:00:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO

-- ================================================================
-- Vie 3/04: LÍMITE EXACTO - 3 slots consecutivos con 54 pre-cargadas
-- Objetivo: agregar 1 reserva cap.1 en cualquiera de los 3 slots
--           para comprobar que pasa de 54 → 55 y se bloquea
-- ================================================================
INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 54
    1, 1,
    'Av. Límite 08h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. Destino 08h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    10.00 + (n % 15),
    25 + (n % 20), 60, 80, 1,
    5.00, 25.00, 'RESERVADO', 'ACTIVO', 'PENDIENTE',
    '08:00:00', '09:00:00', '2026-04-03',
    NULL, NULL, '2026-04-02 08:00:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO

INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 54
    1, 1,
    'Av. Límite 09h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. Destino 09h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    11.00 + (n % 15),
    27 + (n % 20), 60, 80, 1,
    5.00, 25.00, 'RESERVADO', 'ACTIVO', 'PENDIENTE',
    '09:00:00', '10:00:00', '2026-04-03',
    NULL, NULL, '2026-04-02 08:30:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO

INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 54
    1, 1,
    'Av. Límite 10h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. Destino 10h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    12.00 + (n % 15),
    29 + (n % 20), 60, 80, 1,
    5.00, 25.00, 'RESERVADO', 'ACTIVO', 'PENDIENTE',
    '10:00:00', '11:00:00', '2026-04-03',
    NULL, NULL, '2026-04-02 09:00:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO

-- ================================================================
-- Sab 4/04: LÍMITE EXACTO - 3 slots consecutivos con 53 pre-cargadas
-- Objetivo: agregar 1 → estado CRÍTICO (54/55 = 98%)
--           agregar 2 → se bloquea (55/55)
-- ================================================================
INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 53
    1, 1,
    'Av. Crítico 08h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. Destino Crit08 Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    10.00 + (n % 15),
    25 + (n % 20), 60, 80, 1,
    5.00, 25.00, 'RESERVADO', 'ACTIVO', 'PENDIENTE',
    '08:00:00', '09:00:00', '2026-04-04',
    NULL, NULL, '2026-04-03 08:00:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO

INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 53
    1, 1,
    'Av. Crítico 09h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. Destino Crit09 Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    11.00 + (n % 15),
    27 + (n % 20), 60, 80, 1,
    5.00, 25.00, 'RESERVADO', 'ACTIVO', 'PENDIENTE',
    '09:00:00', '10:00:00', '2026-04-04',
    NULL, NULL, '2026-04-03 08:30:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO

INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 53
    1, 1,
    'Av. Crítico 10h Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. Destino Crit10 Lote ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    12.00 + (n % 15),
    29 + (n % 20), 60, 80, 1,
    5.00, 25.00, 'RESERVADO', 'ACTIVO', 'PENDIENTE',
    '10:00:00', '11:00:00', '2026-04-04',
    NULL, NULL, '2026-04-03 09:00:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO

-- Jue 2/04: slot 08:00 lleno para cap.2 (43 reservas cap.2, NroBloques=1)
INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 43
    2, 1,
    'Av. Origen Jue ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. Destino Jue ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    11.00 + (n % 18),
    27 + (n % 28), 60, 80, 1,
    5.00, 25.00, 'RESERVADO', 'ACTIVO', 'PENDIENTE',
    '08:00:00', '09:00:00', '2026-04-02',
    NULL, NULL, '2026-03-31 08:00:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO

-- ================================================================
-- SEMANA 3: CANCELACIONES (7-12 Abril)
-- Lun 7/04: 55 en slot 11:00, pero 15 CANCELADO
-- Resultado: 40 activas < 55 → slot sigue visible
-- ================================================================
INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 55
    1, 1,
    'Av. Origen Cancel ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. Destino Cancel ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    9.00 + (n % 14),
    22 + (n % 22), 60, 80, 1,
    5.00, 25.00,
    -- Las primeras 15 serán CANCELADO, el resto CONFIRMADO
    CASE WHEN n <= 15 THEN 'CANCELADO' ELSE 'RESERVADO' END,
    'ACTIVO', 'PENDIENTE',
    '11:00:00', '12:00:00', '2026-04-07',
    NULL, NULL, '2026-04-05 08:00:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO

-- Mar 8/04: 43 en slot 13:00, 10 CANCELADO → 33 activas < 43 → libre para cap.2
INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 43
    2, 1,
    'Av. Origen Cancel2 ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. Destino Cancel2 ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    13.00 + (n % 16),
    32 + (n % 24), 60, 80, 1,
    5.00, 25.00,
    CASE WHEN n <= 10 THEN 'CANCELADO' ELSE 'RESERVADO' END,
    'ACTIVO', 'PENDIENTE',
    '13:00:00', '14:00:00', '2026-04-08',
    NULL, NULL, '2026-04-06 08:00:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO

-- Mie 9/04: operación normal con mezcla de cancelaciones
INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
VALUES
(1,1,'Av. Próceres 500','-12.0200','-76.9780','SJL Centro','-11.9920','-76.9820',13.20,33,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','08:00:00','10:00:00','2026-04-09',NULL,NULL,'2026-04-07 09:00:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Grau 2400','-12.0560','-77.0160','La Victoria Sur','-12.0800','-77.0020',9.40,23,60,80,1,5.00,25.00,'CANCELADO','ACTIVO','PENDIENTE','08:00:00','09:00:00','2026-04-09',NULL,NULL,'2026-04-07 09:10:00',NULL,803,NULL,'DISPONIBLE'),
(2,1,'Calle Ucayali 300','-12.0480','-77.0270','Rímac Norte','-12.0250','-77.0310',8.20,20,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','12:00:00','13:00:00','2026-04-09',NULL,NULL,'2026-04-07 09:20:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Universitaria 3800','-11.9700','-77.0620','Comas Sur','-11.9510','-77.0510',10.60,26,60,80,1,5.00,25.00,'CANCELADO','ACTIVO','PENDIENTE','14:00:00','15:00:00','2026-04-09',NULL,NULL,'2026-04-07 09:30:00',NULL,803,NULL,'DISPONIBLE'),
(3,1,'Av. Javier Prado 2800','-12.0870','-77.0200','San Borja Sur','-12.1060','-76.9980',12.30,31,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','16:00:00','17:00:00','2026-04-09',NULL,NULL,'2026-04-07 09:40:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Jr. Andahuaylas 700','-12.0540','-77.0140','La Victoria Industrial','-12.0690','-77.0020',7.80,19,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','19:00:00','20:00:00','2026-04-09',NULL,NULL,'2026-04-07 09:50:00',NULL,803,NULL,'DISPONIBLE'),
(2,1,'Av. Tupac Amaru 4500','-11.9650','-77.0540','Carabayllo','-11.8920','-77.0340',28.40,71,60,80,3,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','20:00:00','23:00:00','2026-04-09',NULL,NULL,'2026-04-07 10:00:00',NULL,803,NULL,'DISPONIBLE');
GO

-- ================================================================
-- SEMANA 4: DISTINTAS CAPACIDADES (14-19 Abril)
-- Lun 14/04: 11 reservas cap.3 en slot 11:00
-- Resultado: slot bloqueado SOLO para cap.3, libre para cap.1 y cap.2
-- ================================================================
INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 11
    3, 1,
    'Av. Origen Cap3 ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. Destino Cap3 ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    20.00 + (n % 25),
    50 + (n % 40), 60, 80, 2,
    5.00, 25.00, 'RESERVADO', 'ACTIVO', 'PENDIENTE',
    '11:00:00', '13:00:00', '2026-04-14',
    NULL, NULL, '2026-04-12 08:00:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO

-- Mar 15/04: 43 reservas cap.2 en slot 09:00
-- Resultado: slot bloqueado para cap.2 y cap.3, libre para cap.1 (55-43=12 grúas)
INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
SELECT TOP 43
    2, 1,
    'Av. Origen Mar15 ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.0464', '-77.0428',
    'Av. Destino Mar15 ' + CAST(n AS VARCHAR(3)) + ', Lima',
    '-12.1000', '-77.0500',
    14.00 + (n % 20),
    35 + (n % 30), 60, 80, 1,
    5.00, 25.00, 'RESERVADO', 'ACTIVO', 'PENDIENTE',
    '09:00:00', '10:00:00', '2026-04-15',
    NULL, NULL, '2026-04-13 08:00:00', NULL, 803, NULL, 'DISPONIBLE'
FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
      FROM sys.objects a CROSS JOIN sys.objects b) nums;
GO

-- Mie 16/04: mezcla realista de capacidades
INSERT INTO dbo.Reserva (
    CantidadCarga, IdCliente, DireccionOrigen, CoordLatOrigen, CoordLonOrigen,
    DireccionDestino, CoordLatDestino, CoordLonDestino, DistanciaKm,
    TiempoEstimado, TiempoManiobra, TiempoRetorno, NroBloques,
    CostoKm, CostoBase, EstadoOperacion, Estado, EstadoAdministrativo,
    HoraInicio, HoraFin, FechaServicio, IdOperador, IdGrua,
    FechaCreacion, FechaActualizacion, CreadoPor, ActualizadoPor, TipoHorario)
VALUES
(1,1,'Av. Próceres 120','-12.0200','-76.9780','SJL Norte','-11.9850','-76.9820',14.60,37,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','08:00:00','10:00:00','2026-04-16',NULL,NULL,'2026-04-14 09:00:00',NULL,803,NULL,'DISPONIBLE'),
(2,1,'Av. Industrial 800','-12.0580','-77.0730','Callao Sur','-12.0700','-77.1300',16.20,40,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','10:00:00','12:00:00','2026-04-16',NULL,NULL,'2026-04-14 09:10:00',NULL,803,NULL,'DISPONIBLE'),
(3,1,'Jr. Camaná 400','-12.0480','-77.0340','San Isidro Norte','-12.0930','-77.0420',11.80,29,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','12:00:00','13:00:00','2026-04-16',NULL,NULL,'2026-04-14 09:20:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Benavides 1800','-12.1090','-77.0210','Chorrillos Centro','-12.1560','-77.0190',11.40,28,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','14:00:00','15:00:00','2026-04-16',NULL,NULL,'2026-04-14 09:30:00',NULL,803,NULL,'DISPONIBLE'),
(2,1,'Calle Huancavelica 300','-12.0510','-77.0320','La Victoria Norte','-12.0630','-77.0060',8.30,21,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','16:00:00','17:00:00','2026-04-16',NULL,NULL,'2026-04-14 09:40:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Javier Prado Este 600','-12.0890','-76.9810','La Molina Centro','-12.0840','-76.9410',14.90,37,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','18:00:00','20:00:00','2026-04-16',NULL,NULL,'2026-04-14 09:50:00',NULL,803,NULL,'DISPONIBLE'),
(3,1,'Av. Separadora Industrial 1200','-12.0340','-76.9620','Ate Industrial','-12.0220','-76.9260',18.70,47,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','20:00:00','22:00:00','2026-04-16',NULL,NULL,'2026-04-14 10:00:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Alfredo Benavides 4200','-12.1330','-76.9750','Santiago de Surco','-12.1450','-76.9920',9.20,23,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','22:00:00','23:00:00','2026-04-16',NULL,NULL,'2026-04-14 10:10:00',NULL,803,NULL,'DISPONIBLE'),
-- Jue 17/04
(2,1,'Av. Túpac Amaru 3600','-11.9710','-77.0540','Independencia Sur','-11.9990','-77.0620',10.20,25,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','08:00:00','09:00:00','2026-04-17',NULL,NULL,'2026-04-15 09:00:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Colonial 1600','-12.0580','-77.0920','Callao Centro','-12.0580','-77.1280',12.50,31,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','11:00:00','12:00:00','2026-04-17',NULL,NULL,'2026-04-15 09:10:00',NULL,803,NULL,'DISPONIBLE'),
(3,1,'Jr. Puno 600','-12.0520','-77.0280','La Perla Callao','-12.0730','-77.1210',19.80,50,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','13:00:00','15:00:00','2026-04-17',NULL,NULL,'2026-04-15 09:20:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. La Fontana 800','-12.0810','-76.9730','La Molina Norte','-12.0780','-76.9440',11.40,28,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','15:00:00','16:00:00','2026-04-17',NULL,NULL,'2026-04-15 09:30:00',NULL,803,NULL,'DISPONIBLE'),
(2,1,'Calle Manuel Cipriano Dulanto','-12.0800','-77.0950','La Perla Industrial','-12.0680','-77.1100',8.60,21,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','17:00:00','18:00:00','2026-04-17',NULL,NULL,'2026-04-15 09:40:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Angamos Oeste 1400','-12.1120','-77.0380','San Borja Oeste','-12.1050','-77.0090',9.70,24,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','20:00:00','21:00:00','2026-04-17',NULL,NULL,'2026-04-15 09:50:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. San Borja Norte 700','-12.1000','-76.9990','Surco Este','-12.1230','-76.9820',10.30,26,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','21:00:00','22:00:00','2026-04-17',NULL,NULL,'2026-04-15 10:00:00',NULL,803,NULL,'DISPONIBLE'),
-- Vie 18/04
(1,1,'Av. Arequipa 5200','-12.1180','-77.0280','Miraflores Sur','-12.1330','-77.0220',7.80,19,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','08:00:00','09:00:00','2026-04-18',NULL,NULL,'2026-04-16 09:00:00',NULL,803,NULL,'DISPONIBLE'),
(2,1,'Av. Del Ejercito 2400','-12.1080','-77.0590','San Miguel Sur','-12.0840','-77.0870',12.10,30,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','10:00:00','11:00:00','2026-04-18',NULL,NULL,'2026-04-16 09:10:00',NULL,803,NULL,'DISPONIBLE'),
(3,1,'Av. Venezuela 1800','-12.0610','-77.0780','Breña Industrial','-12.0620','-77.0640',5.40,13,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','12:00:00','13:00:00','2026-04-18',NULL,NULL,'2026-04-16 09:20:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Jr. Zorritos 1200','-12.0570','-77.0520','Breña Centro','-12.0650','-77.0470',5.80,14,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','14:00:00','15:00:00','2026-04-18',NULL,NULL,'2026-04-16 09:30:00',NULL,803,NULL,'DISPONIBLE'),
(2,1,'Av. Brasil 2400','-12.0790','-77.0680','Pueblo Libre Sur','-12.0820','-77.0780',4.60,11,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','17:00:00','18:00:00','2026-04-18',NULL,NULL,'2026-04-16 09:40:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Universitaria 1800','-12.0870','-77.0660','San Miguel Norte','-12.0770','-77.0910',8.90,22,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','19:00:00','20:00:00','2026-04-18',NULL,NULL,'2026-04-16 09:50:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Calle Coronel Inclán 500','-12.1300','-77.0090','Surquillo Norte','-12.1130','-77.0040',8.10,20,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','21:00:00','22:00:00','2026-04-18',NULL,NULL,'2026-04-16 10:00:00',NULL,803,NULL,'DISPONIBLE'),
-- Sab 19/04
(1,1,'Av. Próceres 3200','-12.0200','-76.9780','SJL Sur','-11.9960','-76.9830',13.90,35,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','08:00:00','10:00:00','2026-04-19',NULL,NULL,'2026-04-17 09:00:00',NULL,803,NULL,'DISPONIBLE'),
(2,1,'Av. Wiesse 1400','-12.0000','-76.9820','SJL Centro','-12.0080','-76.9770',5.60,14,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','10:00:00','11:00:00','2026-04-19',NULL,NULL,'2026-04-17 09:10:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Huarochirí 800','-12.0320','-76.9580','Ate Este','-12.0130','-76.9210',16.80,42,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','12:00:00','14:00:00','2026-04-19',NULL,NULL,'2026-04-17 09:20:00',NULL,803,NULL,'DISPONIBLE'),
(3,1,'Av. La Molina 1200','-12.0840','-76.9480','Cieneguilla','-12.0530','-76.8720',24.10,60,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','14:00:00','16:00:00','2026-04-19',NULL,NULL,'2026-04-17 09:30:00',NULL,803,NULL,'DISPONIBLE'),
(1,1,'Av. Separadora 2600','-12.0350','-76.9640','Ate Industrial Sur','-12.0240','-76.9300',15.20,38,60,80,2,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','16:00:00','18:00:00','2026-04-19',NULL,NULL,'2026-04-17 09:40:00',NULL,803,NULL,'DISPONIBLE'),
(2,1,'Jr. Los Ciruelos 300','-12.0800','-76.9670','La Molina Residencial','-12.0920','-76.9500',8.30,21,60,80,1,5.00,25.00,'RESERVADO','ACTIVO','PENDIENTE','18:00:00','19:00:00','2026-04-19',NULL,NULL,'2026-04-17 09:50:00',NULL,803,NULL,'DISPONIBLE');
GO

-- ================================================================
-- VERIFICACIÓN: Conteo final por semana
-- ================================================================
SELECT
    DATEPART(WEEK, FechaServicio) AS Semana,
    MIN(FechaServicio)            AS FechaDesde,
    MAX(FechaServicio)            AS FechaHasta,
    COUNT(*)                      AS TotalReservas,
    SUM(CASE WHEN EstadoOperacion = 'CANCELADO' THEN 1 ELSE 0 END) AS Canceladas,
    SUM(CASE WHEN EstadoOperacion = 'RESERVADO' THEN 1 ELSE 0 END) AS Confirmadas
FROM dbo.Reserva
WHERE FechaServicio BETWEEN '2026-03-23' AND '2026-04-19'
GROUP BY DATEPART(WEEK, FechaServicio)
ORDER BY Semana;
GO


-- ================================================================
-- CONSULTA DE MONITOREO: DISPONIBILIDAD POR HORA
-- Ingrese la fecha a analizar en @Fecha
-- ================================================================
DECLARE @Fecha DATE = '2026-03-30';

DECLARE @G1 INT = (SELECT COUNT(*) FROM dbo.Grua WHERE Estado='ACTIVO' AND EstadoOperacion='OPERATIVA' AND Capacidad=1);
DECLARE @G2 INT = (SELECT COUNT(*) FROM dbo.Grua WHERE Estado='ACTIVO' AND EstadoOperacion='OPERATIVA' AND Capacidad=2);
DECLARE @G3 INT = (SELECT COUNT(*) FROM dbo.Grua WHERE Estado='ACTIVO' AND EstadoOperacion='OPERATIVA' AND Capacidad=3);

DECLARE @Inicio DATETIME = CAST(@Fecha AS DATETIME) + CAST('08:00:00' AS DATETIME);

;WITH Horas AS (
    SELECT 0 AS Ofs UNION ALL SELECT Ofs+1 FROM Horas WHERE Ofs < 15
),
Slots AS (
    SELECT DATEADD(HOUR, Ofs, @Inicio) AS SlotDT FROM Horas
),
Ocupacion AS (
    SELECT
        s.SlotDT,

        -- Reservas por nivel exacto
        (SELECT COUNT(*) FROM dbo.Reserva r
         WHERE r.FechaServicio=@Fecha AND r.EstadoOperacion<>'CANCELADO' AND r.CantidadCarga=1
           AND s.SlotDT >= CAST(r.FechaServicio AS DATETIME)+CAST(r.HoraInicio AS DATETIME)
           AND s.SlotDT <  CASE WHEN r.HoraFin<=r.HoraInicio THEN CAST(r.FechaServicio AS DATETIME)+CAST(r.HoraFin AS DATETIME)+1
                                ELSE CAST(r.FechaServicio AS DATETIME)+CAST(r.HoraFin AS DATETIME) END) AS ReservadasCap1,

        (SELECT COUNT(*) FROM dbo.Reserva r
         WHERE r.FechaServicio=@Fecha AND r.EstadoOperacion<>'CANCELADO' AND r.CantidadCarga=2
           AND s.SlotDT >= CAST(r.FechaServicio AS DATETIME)+CAST(r.HoraInicio AS DATETIME)
           AND s.SlotDT <  CASE WHEN r.HoraFin<=r.HoraInicio THEN CAST(r.FechaServicio AS DATETIME)+CAST(r.HoraFin AS DATETIME)+1
                                ELSE CAST(r.FechaServicio AS DATETIME)+CAST(r.HoraFin AS DATETIME) END) AS ReservadasCap2,

        (SELECT COUNT(*) FROM dbo.Reserva r
         WHERE r.FechaServicio=@Fecha AND r.EstadoOperacion<>'CANCELADO' AND r.CantidadCarga=3
           AND s.SlotDT >= CAST(r.FechaServicio AS DATETIME)+CAST(r.HoraInicio AS DATETIME)
           AND s.SlotDT <  CASE WHEN r.HoraFin<=r.HoraInicio THEN CAST(r.FechaServicio AS DATETIME)+CAST(r.HoraFin AS DATETIME)+1
                                ELSE CAST(r.FechaServicio AS DATETIME)+CAST(r.HoraFin AS DATETIME) END) AS ReservadasCap3,

        -- Timers activos por nivel exacto
        (SELECT COUNT(*) FROM dbo.TimerReserva tr
         WHERE tr.FechaServicio=@Fecha AND tr.CantidadCarga=1
           AND DATEADD(MINUTE,tr.TimerExpiracion,tr.FechaCreacion)>GETDATE()
           AND s.SlotDT >= CAST(tr.FechaServicio AS DATETIME)+CAST(tr.HoraInicio AS DATETIME)
           AND s.SlotDT <  CASE WHEN tr.HoraFin<=tr.HoraInicio THEN CAST(tr.FechaServicio AS DATETIME)+CAST(tr.HoraFin AS DATETIME)+1
                                ELSE CAST(tr.FechaServicio AS DATETIME)+CAST(tr.HoraFin AS DATETIME) END) AS TimerReservaCap1,

        (SELECT COUNT(*) FROM dbo.TimerReserva tr
         WHERE tr.FechaServicio=@Fecha AND tr.CantidadCarga=2
           AND DATEADD(MINUTE,tr.TimerExpiracion,tr.FechaCreacion)>GETDATE()
           AND s.SlotDT >= CAST(tr.FechaServicio AS DATETIME)+CAST(tr.HoraInicio AS DATETIME)
           AND s.SlotDT <  CASE WHEN tr.HoraFin<=tr.HoraInicio THEN CAST(tr.FechaServicio AS DATETIME)+CAST(tr.HoraFin AS DATETIME)+1
                                ELSE CAST(tr.FechaServicio AS DATETIME)+CAST(tr.HoraFin AS DATETIME) END) AS TimerReservaCap2,

        (SELECT COUNT(*) FROM dbo.TimerReserva tr
         WHERE tr.FechaServicio=@Fecha AND tr.CantidadCarga=3
           AND DATEADD(MINUTE,tr.TimerExpiracion,tr.FechaCreacion)>GETDATE()
           AND s.SlotDT >= CAST(tr.FechaServicio AS DATETIME)+CAST(tr.HoraInicio AS DATETIME)
           AND s.SlotDT <  CASE WHEN tr.HoraFin<=tr.HoraInicio THEN CAST(tr.FechaServicio AS DATETIME)+CAST(tr.HoraFin AS DATETIME)+1
                                ELSE CAST(tr.FechaServicio AS DATETIME)+CAST(tr.HoraFin AS DATETIME) END) AS TimerReservaCap3
    FROM Slots s
),
-- Demanda total por nivel (reservas + timers)
Demanda AS (
    SELECT *,
        ReservadasCap1 + TimerReservaCap1 AS D1,
        ReservadasCap2 + TimerReservaCap2 AS D2,
        ReservadasCap3 + TimerReservaCap3 AS D3
    FROM Ocupacion
)
SELECT
    CAST(SlotDT AS TIME)                                    AS Slot,

    -- CAP 1
    ReservadasCap1,
    TimerReservaCap1,
    @G1                                                     AS GruasCap1,
    -- Pool 1: solo absorbe D1, lo que sobra queda libre
    GREATEST(0, @G1 - D1)                                   AS GruasDISPONIBLEsCap1,

    -- CAP 2
    ReservadasCap2,
    TimerReservaCap2,
    @G2                                                     AS GruasCap2,
    -- Pool 2: absorbe D2 + overflow de pool 1 (D1 que no cupo en G1)
    GREATEST(0, @G2 - D2 - GREATEST(0, D1 - @G1))          AS GruasDISPONIBLEsCap2,

    -- CAP 3
    ReservadasCap3,
    TimerReservaCap3,
    @G3                                                     AS GruasCap3,
    -- Pool 3: absorbe D3 + overflow de pool 2 (que ya incluye overflow de pool 1)
    GREATEST(0, @G3 - D3
        - GREATEST(0, D2 + GREATEST(0, D1 - @G1) - @G2))   AS GruasDISPONIBLEsCap3

FROM Demanda
ORDER BY SlotDT
OPTION (MAXRECURSION 100);

