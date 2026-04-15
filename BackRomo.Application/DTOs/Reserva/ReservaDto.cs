namespace BackRomo.Application.DTOs.Reserva;

public class ReservaDto
{
    public int       Id               { get; set; }
    public string    DireccionOrigen  { get; set; } = string.Empty;
    public string    CoordLatOrigen   { get; set; } = string.Empty;
    public string    CoordLonOrigen   { get; set; } = string.Empty;
    public string    DireccionDestino { get; set; } = string.Empty;
    public string    CoordLatDestino  { get; set; } = string.Empty;
    public string    CoordLonDestino  { get; set; } = string.Empty;
    public short     CantidadCarga    { get; set; }
    public DateTime  FechaServicio    { get; set; }
    public TimeSpan  HoraInicio       { get; set; }
    public TimeSpan  HoraFin          { get; set; }
    public int       NroBloques       { get; set; }
    public decimal   DistanciaKm      { get; set; }
    public int       TiempoEstimado   { get; set; }
    public int       TiempoManiobra   { get; set; }
    public int       TiempoRetorno    { get; set; }
    public string    EstadoOperacion  { get; set; } = string.Empty;
    public string    Estado           { get; set; } = string.Empty;
    public string?   NombreCliente    { get; set; }
    public string?   GruaAsignada     { get; set; }
    public string?   OperadorAsignado { get; set; }
    public List<VehiculoItemDto> Vehiculos { get; set; } = new();
}
