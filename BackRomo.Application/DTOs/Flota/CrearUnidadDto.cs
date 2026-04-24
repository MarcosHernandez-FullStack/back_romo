namespace BackRomo.Application.DTOs.Flota;

public class CrearUnidadDto
{
    public string   Placa          { get; set; } = string.Empty;
    public string   Marca          { get; set; } = string.Empty;
    public string   Modelo         { get; set; } = string.Empty;
    public short    AñoFabricacion { get; set; }
    public short    Capacidad      { get; set; }
    public DateOnly FecVenSeg      { get; set; }
    public int      CreadoPor      { get; set; }  // se asigna desde el JWT en el controller
}
