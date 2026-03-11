namespace BackRomo.Application.DTOs.Configuracion;

public class ParametroDto
{
    public int      TiempoMargenManiobra    { get; set; }
    public int      TiempoRetornoBase       { get; set; }
    public decimal  UmbralLargaDistancia    { get; set; }
    public int      TiempoTolerancia        { get; set; }
    public DateTime FechaCreacion           { get; set; }
    public DateTime FechaActualizacion      { get; set; }
    public string   CreadoPor               { get; set; } = string.Empty;
    public string   ActualizadoPor          { get; set; } = string.Empty;
    public string   Estado                  { get; set; } = string.Empty;
    public int      TiempoCorte             { get; set; }
    public int      TimerAdministrativo     { get; set; }
    public int      TimerCliente            { get; set; }
    public string   ZonaHoraria            { get; set; } = string.Empty;
}
