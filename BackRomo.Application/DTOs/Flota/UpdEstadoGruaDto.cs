using System.ComponentModel.DataAnnotations;

namespace BackRomo.Application.DTOs.Flota;

public class UpdEstadoGruaDto
{
    public int    IdGrua         { get; set; }  // from route

    [Required(ErrorMessage = "El nuevo estado es obligatorio.")]
    [RegularExpression("^(ACTIVO|INACTIVO)$", ErrorMessage = "El estado debe ser ACTIVO o INACTIVO.")]
    public string NuevoEstado    { get; set; } = string.Empty;

    public int    ActualizadoPor { get; set; }  // set from JWT in controller
}
