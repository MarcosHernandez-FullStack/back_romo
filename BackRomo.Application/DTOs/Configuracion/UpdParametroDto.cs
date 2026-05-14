namespace BackRomo.Application.DTOs.Configuracion;

public class UpdParametroDto
{
    public short   TiempoMargenManiobra { get; set; }
    public short   TiempoRetornoBase    { get; set; }
    public decimal UmbralLargaDistancia { get; set; }
    public short   TiempoTolerancia     { get; set; }
    public short   TiempoCorte          { get; set; }
    public short   TimerAdministrativo  { get; set; }
    public short   TimerCliente         { get; set; }
    public string  ZonaHoraria          { get; set; } = string.Empty;
    public short   MinutosCerca         { get; set; }
    public short   MinutosMedio         { get; set; }
    public string  CoordLatMaps         { get; set; } = string.Empty;
    public string  CoordLonMaps         { get; set; } = string.Empty;
    public decimal MetrosCercania       { get; set; }
}
