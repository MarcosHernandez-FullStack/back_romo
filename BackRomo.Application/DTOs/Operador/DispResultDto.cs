namespace BackRomo.Application.DTOs.Operador;

public class DispResultDto
{
    public int                    Exitoso    { get; set; }
    public string                 Mensaje    { get; set; } = string.Empty;
    public List<DispConflictoDto>? Conflictos { get; set; }
}

public class DispConflictoDto
{
    public int    IdReserva     { get; set; }
    public string FechaServicio { get; set; } = string.Empty;
    public string HoraInicio    { get; set; } = string.Empty;
}
