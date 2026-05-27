using System.ComponentModel.DataAnnotations;

namespace BackRomo.Application.DTOs.Cliente;

public class EditarClienteDto
{
    public int IdCliente { get; set; }  // from route

    [MaxLength(100, ErrorMessage = "La contraseña no puede superar los 100 caracteres.")]
    public string? Contrasena { get; set; }  // null/empty = conservar contraseña actual

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

    public decimal TarifaBase { get; set; }
    public decimal TarifaKm   { get; set; }

    public int ActualizadoPor { get; set; }  // set from JWT in controller
}
