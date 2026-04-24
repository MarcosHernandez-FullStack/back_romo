namespace BackRomo.Application.DTOs.Flota;

public class ReservaALiberarDto
{
    public int    Id               { get; set; }
    public string FechaServicio    { get; set; } = string.Empty;
    public string HoraInicio       { get; set; } = string.Empty;
    public string NombreCliente    { get; set; } = string.Empty;
    public string EstadoOperacion  { get; set; } = string.Empty;
}
