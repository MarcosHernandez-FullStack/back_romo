namespace BackRomo.Application.DTOs.Agenda;

public class UpdConfiguracionHorarioDto
{
    public List<HorarioItemDto> Horarios       { get; set; } = new();
    public int                  ActualizadoPor { get; set; }
}

public class HorarioItemDto
{
    public int    Id         { get; set; }
    public string Estado     { get; set; } = string.Empty;
    public string HoraInicio { get; set; } = string.Empty;
    public string HoraFinal  { get; set; } = string.Empty;
}
