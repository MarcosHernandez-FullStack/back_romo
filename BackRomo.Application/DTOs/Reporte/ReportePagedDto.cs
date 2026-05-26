namespace BackRomo.Application.DTOs.Reporte;

public class ReportePagedDto
{
    public long                    Total       { get; set; }
    public long                    Finalizados { get; set; }
    public long                    Cancelados  { get; set; }
    public decimal                 MontoTotal  { get; set; }
    public long                    Pendientes  { get; set; }
    public long                    Facturados  { get; set; }
    public long                    Pagados     { get; set; }
    public IEnumerable<ReporteDto> Datos       { get; set; } = [];
}
