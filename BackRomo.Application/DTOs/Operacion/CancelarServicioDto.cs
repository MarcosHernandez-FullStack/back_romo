using System.ComponentModel.DataAnnotations;

namespace BackRomo.Application.DTOs.Operacion;

public class CancelarServicioDto
{
    public int    Id                { get; set; }
    [MaxLength(255, ErrorMessage = "El motivo de cancelación no puede superar los 255 caracteres.")]
    public string MotivoCancelacion { get; set; } = string.Empty;
    public int    ActualizadoPor    { get; set; }
}
