namespace BackRomo.Application.DTOs.Agenda;

public class ExcepcionDto
{
    public int    Id                { get; set; }
    public string Fecha             { get; set; } = string.Empty;
    public string Motivo            { get; set; } = string.Empty;
    public string Alcance           { get; set; } = string.Empty;
    public string TiempoInicio      { get; set; } = string.Empty;
    public string TiempoFinal       { get; set; } = string.Empty;
    public string DescripcionMotivo { get; set; } = string.Empty;
    public string Estado            { get; set; } = string.Empty;
}
