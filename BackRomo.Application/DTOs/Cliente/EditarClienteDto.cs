namespace BackRomo.Application.DTOs.Cliente;

public class EditarClienteDto
{
    public int     IdCliente      { get; set; }  // from route
    public string? Contrasena     { get; set; }  // null/empty = conservar contraseña actual
    public string  Empresa        { get; set; } = string.Empty;
    public string  NomContacto    { get; set; } = string.Empty;
    public string? NroContacto    { get; set; }
    public string  CorreoContacto { get; set; } = string.Empty;
    public decimal TarifaBase     { get; set; }
    public decimal TarifaKm       { get; set; }
    public int     ActualizadoPor { get; set; }  // set from JWT in controller
}
