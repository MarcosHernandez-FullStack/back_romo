using System.ComponentModel.DataAnnotations;

namespace BackRomo.Application.DTOs.Agenda;

public class CrearExcepcionDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria.")]
    public string Fecha { get; set; } = string.Empty;

    [Required(ErrorMessage = "El motivo es obligatorio.")]
    [MaxLength(50, ErrorMessage = "El motivo no puede superar los 50 caracteres.")]
    public string Motivo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La hora de inicio es obligatoria.")]
    public string HoraInicio { get; set; } = string.Empty;

    [Required(ErrorMessage = "La hora de fin es obligatoria.")]
    public string HoraFin { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripción del motivo es obligatoria.")]
    [MaxLength(100, ErrorMessage = "La descripción no puede superar los 100 caracteres.")]
    public string DescripcionMotivo { get; set; } = string.Empty;

    public int UsuarioId { get; set; }  // se asigna en el controller desde el JWT
}
