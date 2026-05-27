using System.ComponentModel.DataAnnotations;

namespace BackRomo.Application.DTOs.Usuario;

public class EditarUsuarioDto
{
    public int IdUsuario { get; set; }  // from route

    [MaxLength(100, ErrorMessage = "La contraseña no puede superar los 100 caracteres.")]
    public string? Contrasena { get; set; }  // null/empty = conservar contraseña actual

    [Required(ErrorMessage = "Los nombres son obligatorios.")]
    [MaxLength(100, ErrorMessage = "Los nombres no pueden superar los 100 caracteres.")]
    public string Nombres { get; set; } = string.Empty;

    [Required(ErrorMessage = "Los apellidos son obligatorios.")]
    [MaxLength(100, ErrorMessage = "Los apellidos no pueden superar los 100 caracteres.")]
    public string Apellidos { get; set; } = string.Empty;

    [MaxLength(50, ErrorMessage = "El teléfono no puede superar los 50 caracteres.")]
    public string? Telefono { get; set; }

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [MaxLength(100, ErrorMessage = "El correo no puede superar los 100 caracteres.")]
    [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
    public string Correo { get; set; } = string.Empty;

    [Required(ErrorMessage = "El rol es obligatorio.")]
    [RegularExpression("^(ADMINISTRADOR|STAFF|CLIENTE|OPERADOR)$",
        ErrorMessage = "El rol debe ser ADMINISTRADOR, STAFF, CLIENTE u OPERADOR.")]
    public string Rol { get; set; } = string.Empty;

    public int ActualizadoPor { get; set; }  // set from JWT in controller
}
