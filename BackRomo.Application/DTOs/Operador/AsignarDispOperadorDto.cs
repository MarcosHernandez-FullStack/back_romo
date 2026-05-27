using System.ComponentModel.DataAnnotations;

namespace BackRomo.Application.DTOs.Operador;

public class AsignarDispOperadorDto
{
    public int IdOperador { get; set; }  // se asigna en el controller desde la ruta

    [Required(ErrorMessage = "La disponibilidad es obligatoria.")]
    public List<DispRangoDto> Disponibilidad { get; set; } = new();

    public bool Confirmar      { get; set; }

    public int  ActualizadoPor { get; set; }  // se asigna en el controller desde el JWT
}

public class DispRangoDto
{
    [Range(1, 7, ErrorMessage = "El número de día debe estar entre 1 y 7.")]
    public int NroDia     { get; set; }

    [Required(ErrorMessage = "El nombre del día es obligatorio.")]
    [MaxLength(9, ErrorMessage = "El nombre del día no puede superar los 9 caracteres.")]
    public string NombreDia  { get; set; } = string.Empty;

    [Required(ErrorMessage = "La hora de inicio es obligatoria.")]
    public string HoraInicio { get; set; } = string.Empty;

    [Required(ErrorMessage = "La hora de fin es obligatoria.")]
    public string HoraFin    { get; set; } = string.Empty;
}
