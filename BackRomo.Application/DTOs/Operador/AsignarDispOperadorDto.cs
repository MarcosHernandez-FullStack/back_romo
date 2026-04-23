namespace BackRomo.Application.DTOs.Operador;

public class AsignarDispOperadorDto
{
    public int              IdOperador      { get; set; }
    public List<DispRangoDto> Disponibilidad { get; set; } = new();
    public bool             Confirmar       { get; set; }
    public int              ActualizadoPor  { get; set; }
}

public class DispRangoDto
{
    public int    NroDia     { get; set; }
    public string NombreDia  { get; set; } = string.Empty;
    public string HoraInicio { get; set; } = string.Empty;
    public string HoraFin    { get; set; } = string.Empty;
}
