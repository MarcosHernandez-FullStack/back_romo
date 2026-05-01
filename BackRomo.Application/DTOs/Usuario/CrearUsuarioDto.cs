namespace BackRomo.Application.DTOs.Usuario;

public class CrearUsuarioDto
{
    public string  Correo     { get; set; } = string.Empty;
    public string  Contrasena { get; set; } = string.Empty;
    public string  Nombres    { get; set; } = string.Empty;
    public string  Apellidos  { get; set; } = string.Empty;
    public string? Telefono   { get; set; }
    public string  Rol        { get; set; } = string.Empty;
    public int     CreadoPor  { get; set; }  // set from JWT in controller
}
