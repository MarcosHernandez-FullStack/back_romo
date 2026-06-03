using System.ComponentModel.DataAnnotations;

namespace BackRomo.Application.DTOs.Cliente;

public class CrearClienteDto
{
    [MaxLength(10, ErrorMessage = "El alias no puede superar los 10 caracteres.")]
    public string? Alias { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [MinLength(8,   ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
    [MaxLength(100, ErrorMessage = "La contraseña no puede superar los 100 caracteres.")]
    public string Contrasena { get; set; } = string.Empty;

    [Required(ErrorMessage = "La empresa es obligatoria.")]
    [MaxLength(100, ErrorMessage = "La empresa no puede superar los 100 caracteres.")]
    public string Empresa { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre de contacto es obligatorio.")]
    [MaxLength(100, ErrorMessage = "El nombre de contacto no puede superar los 100 caracteres.")]
    public string NomContacto { get; set; } = string.Empty;

    [MaxLength(20, ErrorMessage = "El número de contacto no puede superar los 20 caracteres.")]
    public string? NroContacto { get; set; }

    [Required(ErrorMessage = "El correo de contacto es obligatorio.")]
    [MaxLength(100, ErrorMessage = "El correo de contacto no puede superar los 100 caracteres.")]
    [EmailAddress(ErrorMessage = "El correo de contacto no tiene un formato válido.")]
    public string CorreoContacto { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "99999999.99",
        ErrorMessage = "La tarifa base debe ser mayor a 0.")]
    public decimal TarifaBase { get; set; }

    [Range(typeof(decimal), "0.01", "99999999.99",
        ErrorMessage = "La tarifa por km debe ser mayor a 0.")]
    public decimal TarifaKm { get; set; }

    public int CreadoPor { get; set; }  // set from JWT in controller
}
