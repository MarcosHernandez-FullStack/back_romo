using System.ComponentModel.DataAnnotations;

namespace BackRomo.Application.DTOs.Agenda;

public class UpdEstadoExcepcionDto
{
    [Required(ErrorMessage = "El nuevo estado es obligatorio.")]
    [RegularExpression("^(ACTIVO|INACTIVO)$",
        ErrorMessage = "El estado debe ser ACTIVO o INACTIVO.")]
    public string NuevoEstado    { get; set; } = string.Empty;

    public int ActualizadoPor    { get; set; }  // se asigna en el controller desde el JWT
}
