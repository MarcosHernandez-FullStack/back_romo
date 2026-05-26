namespace BackRomo.Application.DTOs.Reserva;

public class ReservaPagedDto
{
    public long                    Total          { get; set; }
    public IEnumerable<ReservaDto> Datos          { get; set; } = [];
    public int                     TotalReservado { get; set; }
    public int                     TotalAsignado  { get; set; }
    public int                     TotalEnCurso   { get; set; }
}
