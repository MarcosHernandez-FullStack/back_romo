using System.ComponentModel.DataAnnotations;

namespace BackRomo.Application.DTOs.Operador;

public class CrearOperadorDto
{
    [MaxLength(10, ErrorMessage = "El alias no puede superar los 10 caracteres.")]
    public string?  Alias       { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [MinLength(8,   ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
    [MaxLength(100, ErrorMessage = "La contraseña no puede superar los 100 caracteres.")]
    public string Contrasena  { get; set; } = string.Empty;  // plain text; se hashea en el repositorio

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [MaxLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
    public string Nombres     { get; set; } = string.Empty;

    [Required(ErrorMessage = "Los apellidos son obligatorios.")]
    [MaxLength(100, ErrorMessage = "Los apellidos no pueden superar los 100 caracteres.")]
    public string Apellidos   { get; set; } = string.Empty;

    [MaxLength(50, ErrorMessage = "El teléfono no puede superar los 50 caracteres.")]
    public string? Telefono   { get; set; }

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [MaxLength(100, ErrorMessage = "El correo no puede superar los 100 caracteres.")]
    [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
    public string Correo      { get; set; } = string.Empty;

    [Required(ErrorMessage = "El rol es obligatorio.")]
    [RegularExpression("^(ADMINISTRADOR|STAFF|CLIENTE|OPERADOR)$",
        ErrorMessage = "El rol debe ser ADMINISTRADOR, STAFF, CLIENTE u OPERADOR.")]
    public string Rol         { get; set; } = "OPERADOR";

    [Required(ErrorMessage = "El número de licencia es obligatorio.")]
    [MaxLength(9, ErrorMessage = "El número de licencia no puede superar los 9 caracteres.")]
    public string NroLicencia { get; set; } = string.Empty;

    public DateOnly FecVenLic { get; set; }

    public int CreadoPor      { get; set; }  // se asigna desde el JWT en el controller
}
