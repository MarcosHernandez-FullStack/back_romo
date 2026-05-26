namespace BackRomo.Application.DTOs.Operador;

public class OperadorPagedDto
{
    public long                          Total                 { get; set; }
    public long                          ConServiciosHoy       { get; set; }
    public long                          ConServiciosAsignados { get; set; }
    public long                          LicenciasVencidas     { get; set; }
    public IEnumerable<OperadorListDto>  Datos                 { get; set; } = [];
}
