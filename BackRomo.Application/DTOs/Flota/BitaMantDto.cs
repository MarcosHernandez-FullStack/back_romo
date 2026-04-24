namespace BackRomo.Application.DTOs.Flota;

public class BitaMantDto
{
    public string  Titulo          { get; set; } = string.Empty;
    public string  FechaCreacion   { get; set; } = string.Empty;
    public string  Responsable     { get; set; } = string.Empty;
    public int     Kilometraje     { get; set; }
    public string? Nota            { get; set; }
    public string  EstadoOperacion { get; set; } = string.Empty;
}
