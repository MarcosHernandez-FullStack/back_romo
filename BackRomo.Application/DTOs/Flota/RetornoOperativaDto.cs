using System.ComponentModel.DataAnnotations;

namespace BackRomo.Application.DTOs.Flota;

public class RetornoOperativaDto
{
    public int     IdGrua            { get; set; }  // from route

    [Required(ErrorMessage = "El nombre del responsable es obligatorio.")]
    [MaxLength(50, ErrorMessage = "El nombre del responsable no puede superar los 50 caracteres.")]
    public string  NombreResponsable { get; set; } = string.Empty;
    public int     Kilometraje       { get; set; }
    public string? Nota              { get; set; }
    public int     ActualizadoPor    { get; set; }  // set from JWT in controller
}
