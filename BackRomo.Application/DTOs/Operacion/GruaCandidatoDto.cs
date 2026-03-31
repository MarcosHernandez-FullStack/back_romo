namespace BackRomo.Application.DTOs.Operacion;

public class GruaCandidatoDto
{
    public int     Id         { get; set; }
    public string  Placa      { get; set; } = string.Empty;
    public string  Marca      { get; set; } = string.Empty;
    public string  Modelo     { get; set; } = string.Empty;
    public short   Capacidad  { get; set; }
    public string? UltLatitud { get; set; }
    public string? UltLongitud{ get; set; }
}
