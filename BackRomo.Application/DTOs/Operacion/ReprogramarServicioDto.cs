namespace BackRomo.Application.DTOs.Operacion;

public class ReprogramarServicioDto
{
    public int      IdReserva       { get; set; }
    public DateTime NuevaFecha      { get; set; }
    public string   NuevaHoraInicio { get; set; } = string.Empty;   // "HH:mm"
    public int      NuevoNroBloques { get; set; }
    public int      ActualizadoPor  { get; set; }
}
