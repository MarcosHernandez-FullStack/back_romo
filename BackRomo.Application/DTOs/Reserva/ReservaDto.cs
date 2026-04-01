namespace BackRomo.Application.DTOs.Reserva;

public class ReservaDto
{
    public int      Id               { get; set; }
    public string   DireccionOrigen  { get; set; } = string.Empty;
    public string   DireccionDestino { get; set; } = string.Empty;
    public short    CantidadCarga    { get; set; }
    public DateTime  FechaServicio    { get; set; }
    public TimeSpan  HoraInicio       { get; set; }
    public TimeSpan  HoraFin          { get; set; }
    public int      NroBloques       { get; set; }
    public string   EstadoOperacion  { get; set; } = string.Empty;
    public string   Estado           { get; set; } = string.Empty;
}
