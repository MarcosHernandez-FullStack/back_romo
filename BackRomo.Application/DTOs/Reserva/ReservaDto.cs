namespace BackRomo.Application.DTOs.Reserva;

public class ReservaDto
{
    public int      Id               { get; set; }
    public string   DireccionOrigen  { get; set; } = string.Empty;
    public string   DireccionDestino { get; set; } = string.Empty;
    public short    CantidadCarga    { get; set; }
    public DateOnly FechaServicio    { get; set; }
    public TimeOnly HoraInicio       { get; set; }
    public TimeOnly HoraFin          { get; set; }
    public int      NroBloques       { get; set; }
    public string   Estado           { get; set; } = string.Empty;
}
