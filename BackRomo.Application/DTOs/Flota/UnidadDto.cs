namespace BackRomo.Application.DTOs.Flota;

public class UnidadDto
{
    public int     Id              { get; set; }
    public string  Placa           { get; set; } = string.Empty;
    public string  Marca           { get; set; } = string.Empty;
    public string  Modelo          { get; set; } = string.Empty;
    public short   AñoFabricacion  { get; set; }
    public short   Capacidad       { get; set; }
    public string? FecVenSeg       { get; set; }
    public string  EstadoOperacion { get; set; } = string.Empty;
    public string  Estado          { get; set; } = string.Empty;
}
