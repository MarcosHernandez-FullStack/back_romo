namespace BackRomo.Application.DTOs.Agenda;

public class CrearExcepcionDto
{
    public int    Id                { get; set; }
    public string Fecha             { get; set; } = string.Empty;
    public string Motivo            { get; set; } = string.Empty;
    public string HoraInicio        { get; set; } = string.Empty;
    public string HoraFin           { get; set; } = string.Empty;
    public string DescripcionMotivo { get; set; } = string.Empty;
    public int    UsuarioId         { get; set; }
}
