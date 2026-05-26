namespace BackRomo.Application.DTOs.Cliente;

public class ClientePagedDto
{
    public long                    Total          { get; set; }
    public long                    TotalActivos   { get; set; }
    public long                    TotalInactivos { get; set; }
    public IEnumerable<ClienteDto> Datos          { get; set; } = [];
}
