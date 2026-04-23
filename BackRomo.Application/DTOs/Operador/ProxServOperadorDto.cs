namespace BackRomo.Application.DTOs.Operador;

public class ProxServOperadorDto
{
    public int    Id             { get; set; }
    public string FechaServicio  { get; set; } = string.Empty;
    public string HoraInicio     { get; set; } = string.Empty;
    public string HoraFin        { get; set; } = string.Empty;
    public string NomCliente     { get; set; } = string.Empty;
    public string FechaAbreviada { get; set; } = string.Empty;
}
