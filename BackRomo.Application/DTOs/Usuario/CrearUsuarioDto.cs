using System.ComponentModel.DataAnnotations;

namespace BackRomo.Application.DTOs.Usuario;

public class CrearUsuarioDto
{
    [Required(ErrorMessage = "El correo es obligatorio.")]
    [MaxLength(100, ErrorMessage = "El correo no puede superar los 100 caracteres.")]
    [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
    public string Correo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [MinLength(8,   ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
    [MaxLength(100, ErrorMessage = "La contraseña no puede superar los 100 caracteres.")]
    public string Contrasena { get; set; } = string.Empty;

    [Required(ErrorMessage = "Los nombres son obligatorios.")]
    [MaxLength(100, ErrorMessage = "Los nombres no pueden superar los 100 caracteres.")]
    public string Nombres { get; set; } = string.Empty;

    [Required(ErrorMessage = "Los apellidos son obligatorios.")]
    [MaxLength(100, ErrorMessage = "Los apellidos no pueden superar los 100 caracteres.")]
    public string Apellidos { get; set; } = string.Empty;

    [MaxLength(10, ErrorMessage = "El alias no puede superar los 10 caracteres.")]
    public string? Alias { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [MaxLength(50, ErrorMessage = "El teléfono no puede superar los 50 caracteres.")]
    public string Telefono { get; set; } = string.Empty;

    [Required(ErrorMessage = "El rol es obligatorio.")]
    [RegularExpression("^(ADMINISTRADOR|STAFF|CLIENTE|OPERADOR)$",
        ErrorMessage = "El rol debe ser ADMINISTRADOR, STAFF, CLIENTE u OPERADOR.")]
    public string Rol { get; set; } = string.Empty;

    public int CreadoPor { get; set; } // set from JWT in controller
}
