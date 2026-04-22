namespace BackRomo.Application.DTOs.Operador;

public class OperadorListDto
{
    public int     Id                      { get; set; }
    public string  Alias                   { get; set; } = string.Empty;
    public string  NombresCompleto         { get; set; } = string.Empty;
    public string? Telefono                { get; set; }
    public string  NroLicencia             { get; set; } = string.Empty;
    public DateOnly FecVenLic              { get; set; }
    public string  Estado                  { get; set; } = string.Empty;
    public DateOnly? ProximaFechaServicio  { get; set; }
    public TimeOnly? ProximaHoraServicio   { get; set; }
    public int     TotalServiciosAsignados { get; set; }
    public int     TotalHorasSemanales     { get; set; }
}
