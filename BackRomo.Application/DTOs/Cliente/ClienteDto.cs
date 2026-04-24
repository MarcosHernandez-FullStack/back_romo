namespace BackRomo.Application.DTOs.Cliente;

public class ClienteDto
{
    public int     Id              { get; set; }
    public string  Empresa         { get; set; } = string.Empty;
    public string  NomContacto     { get; set; } = string.Empty;
    public string? NroContacto     { get; set; }
    public string  CorreoContacto  { get; set; } = string.Empty;
    public decimal TarifaBase      { get; set; }
    public decimal TarifaKm        { get; set; }
    public string  TipoTarifaBase  { get; set; } = string.Empty;
    public string  TipoTarifaKm    { get; set; } = string.Empty;
    public string  Estado          { get; set; } = string.Empty;
    public string  Alias           { get; set; } = string.Empty;
    public string? Contraseña      { get; set; }
}
