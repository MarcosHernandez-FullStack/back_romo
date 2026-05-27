using System.ComponentModel.DataAnnotations;

namespace BackRomo.Application.DTOs.Agenda;

public class UpdConfiguracionHorarioDto
{
    [Required(ErrorMessage = "La lista de horarios es obligatoria.")]
    public List<HorarioItemDto> Horarios       { get; set; } = new();

    public int ActualizadoPor { get; set; }  // se asigna en el controller desde el JWT
}

public class HorarioItemDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio.")]
    [RegularExpression("^(ACTIVO|INACTIVO)$",
        ErrorMessage = "El estado debe ser ACTIVO o INACTIVO.")]
    public string Estado     { get; set; } = string.Empty;

    [Required(ErrorMessage = "La hora de inicio es obligatoria.")]
    public string HoraInicio { get; set; } = string.Empty;

    [Required(ErrorMessage = "La hora final es obligatoria.")]
    public string HoraFinal  { get; set; } = string.Empty;
}
