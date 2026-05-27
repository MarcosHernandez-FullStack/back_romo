using System.ComponentModel.DataAnnotations;

namespace BackRomo.Application.DTOs.Cliente;

public class UpdEstadoClienteDto
{
    public int IdCliente { get; set; }

    [Required(ErrorMessage = "El nuevo estado es obligatorio.")]
    [RegularExpression("^(ACTIVO|INACTIVO)$",
        ErrorMessage = "El estado debe ser ACTIVO o INACTIVO.")]
    public string NuevoEstado { get; set; } = string.Empty;

    public int ActualizadoPor { get; set; }
}
