namespace BackRomo.Application.DTOs.Agenda;

public class ExcepcionPagedDto
{
    public long                      Total { get; set; }
    public IEnumerable<ExcepcionDto> Datos { get; set; } = [];
}
