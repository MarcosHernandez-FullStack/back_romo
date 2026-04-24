namespace BackRomo.Application.DTOs.Flota;

public class EditarUnidadDto
{
    public int      IdGrua         { get; set; }  // se asigna en el controller desde la ruta
    public string   Placa          { get; set; } = string.Empty;
    public string   Marca          { get; set; } = string.Empty;
    public string   Modelo         { get; set; } = string.Empty;
    public short    AñoFabricacion { get; set; }
    public short    Capacidad      { get; set; }
    public DateOnly FecVenSeg      { get; set; }
    public int      ActualizadoPor { get; set; }  // se asigna desde el JWT en el controller
}
