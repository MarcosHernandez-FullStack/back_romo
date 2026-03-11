namespace BackRomo.Application.DTOs.Reserva;

public class ValidarHorarioResultDto
{
    public int    Exitoso { get; set; }
    public string Mensaje { get; set; } = string.Empty;
    public int?   Id      { get; set; }
}
