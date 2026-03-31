namespace BackRomo.Application.DTOs.Operacion;

public class OperadorCandidatoDto
{
    public int     Id         { get; set; }
    public string  Nombres    { get; set; } = string.Empty;
    public string  Apellidos  { get; set; } = string.Empty;
    public string? UltLatitud { get; set; }
    public string? UltLongitud{ get; set; }
}
