namespace BackRomo.Application.DTOs.Reporte;

public class UpdEstadoAdministrativoDto
{
    public string EstadoAdministrativo { get; set; } = string.Empty;
    public int    IdReserva            { get; set; }
    public int    ActualizadoPor       { get; set; }
}
