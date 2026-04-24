namespace BackRomo.Application.DTOs.Cliente;

public class CrearClienteDto
{
    public string  Alias          { get; set; } = string.Empty;
    public string  Contrasena     { get; set; } = string.Empty;
    public string  Empresa        { get; set; } = string.Empty;
    public string  NomContacto    { get; set; } = string.Empty;
    public string? NroContacto    { get; set; }
    public string  CorreoContacto { get; set; } = string.Empty;
    public decimal TarifaBase     { get; set; }
    public decimal TarifaKm       { get; set; }
    public int     CreadoPor      { get; set; }  // set from JWT in controller
}
