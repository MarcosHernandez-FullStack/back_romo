using System.ComponentModel.DataAnnotations;

namespace BackRomo.Application.DTOs.Operacion;

public class ReprogramarServicioDto
{
    public int      IdReserva       { get; set; }
    public DateTime NuevaFecha      { get; set; }
    [Required(ErrorMessage = "La hora de inicio es obligatoria.")]
    [RegularExpression(@"^([01]\d|2[0-3]):[0-5]\d$", ErrorMessage = "La hora de inicio debe tener el formato HH:mm.")]
    public string   NuevaHoraInicio { get; set; } = string.Empty;
    [Range(1, int.MaxValue, ErrorMessage = "El número de bloques debe ser mayor o igual a 1.")]
    public int      NuevoNroBloques { get; set; }
    public int      ActualizadoPor  { get; set; }
    public string   Rol             { get; set; } = "ADMINISTRADOR";
}
