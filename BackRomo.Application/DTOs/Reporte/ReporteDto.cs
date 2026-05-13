namespace BackRomo.Application.DTOs.Reporte;

public class ReporteDto
{
    public string   Id                   { get; set; } = string.Empty;
    public string   Cliente              { get; set; } = string.Empty;
    public decimal  Costo                { get; set; }
    public string   Origen               { get; set; } = string.Empty;
    public string   Destino              { get; set; } = string.Empty;
    public decimal  DistanciaKm          { get; set; }
    public string   Fecha                { get; set; } = string.Empty;
    public string   Hora                 { get; set; } = string.Empty;
    public int      TiempoMin            { get; set; }
    public int      Bloques              { get; set; }
    public int      CantidadCarga        { get; set; }
    public string?  Operador             { get; set; }
    public string?  Unidad               { get; set; }
    public string   Estado               { get; set; } = string.Empty;
    public string   EstadoAdministrativo { get; set; } = string.Empty;
    public string   FechaCompleta        { get; set; } = string.Empty;
    public string   FechaCorta           { get; set; } = string.Empty;
    public string   Grua                 { get; set; } = string.Empty;
    public string?  MotivoCancelacion    { get; set; }
    public string?  CanceladoPor         { get; set; }
}
