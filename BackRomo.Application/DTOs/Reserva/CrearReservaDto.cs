namespace BackRomo.Application.DTOs.Reserva;

public class CrearReservaDto
{
    public DateOnly       FechaServicio     { get; set; }
    public TimeOnly       HoraInicio        { get; set; }
    public TimeOnly       HoraFin           { get; set; }
    public short          CantidadCarga     { get; set; }
    public string         Rol               { get; set; } = string.Empty;
    public int            IdCliente         { get; set; }
    public int?           IdOperador        { get; set; }
    public string         DireccionOrigen   { get; set; } = string.Empty;
    public string         CoordLatOrigen    { get; set; } = string.Empty;
    public string         CoordLonOrigen    { get; set; } = string.Empty;
    public string         DireccionDestino  { get; set; } = string.Empty;
    public string         CoordLatDestino   { get; set; } = string.Empty;
    public string         CoordLonDestino   { get; set; } = string.Empty;
    public decimal        DistanciaKm       { get; set; }
    public int            TiempoEstimado    { get; set; }
    public int            TiempoManiobra    { get; set; }
    public int            TiempoRetorno     { get; set; }
    public int            NroBloques        { get; set; }
    public decimal        CostoKm           { get; set; }
    public decimal        CostoBase         { get; set; }
    public short          TimerExpiracion       { get; set; }
    public int            CreadoPor             { get; set; }
    public string         TipoHorario           { get; set; } = string.Empty;
    public string         EstadoOperacion       { get; set; } = string.Empty;
    public string         EstadoAdministrativo  { get; set; } = string.Empty;
    public string         Estado                { get; set; } = string.Empty;
    public DateTime       FechaCreacion         { get; set; }
}
