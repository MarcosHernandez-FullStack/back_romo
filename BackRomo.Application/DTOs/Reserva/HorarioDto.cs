namespace BackRomo.Application.DTOs.Reserva;

public class HorarioDto
{
    public TimeSpan Hora { get; set; }
    public string Estado { get; set; } = "disponible";
}
