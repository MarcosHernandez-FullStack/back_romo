namespace BackRomo.Application.DTOs.Flota;

public class GruaPagedDto
{
    public long                      Total           { get; set; }
    public long                      Operativas      { get; set; }
    public long                      EnTaller        { get; set; }
    public long                      SegurosCriticos { get; set; }
    public IEnumerable<UnidadDto>    Datos           { get; set; } = [];
}
