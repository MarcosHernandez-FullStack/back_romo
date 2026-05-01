namespace BackRomo.Application.DTOs.Usuario;

public class EditarUsuarioDto
{
    public int     IdUsuario      { get; set; }  // from route
    public string? Contrasena     { get; set; }  // null/empty = conservar contraseña actual
    public string  Nombres        { get; set; } = string.Empty;
    public string  Apellidos      { get; set; } = string.Empty;
    public string? Telefono       { get; set; }
    public string  Correo         { get; set; } = string.Empty;
    public string  Rol            { get; set; } = string.Empty;
    public int     ActualizadoPor { get; set; }  // set from JWT in controller
}
