namespace BackRomo.Application.DTOs.Usuario;

public class UsuarioDto
{
    public int      Id            { get; set; }
    public string   Nombres       { get; set; } = string.Empty;
    public string   Apellidos     { get; set; } = string.Empty;
    public string   Correo        { get; set; } = string.Empty;
    public string?  Telefono      { get; set; }
    public string   Rol           { get; set; } = string.Empty;
    public string   Estado        { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
}
