namespace BackRomo.Application.DTOs.Operacion;

public class GruaSugeridaDto
{
    public int      Id            { get; set; }
    public string   Placa         { get; set; } = string.Empty;
    public string   Marca         { get; set; } = string.Empty;
    public string   Modelo        { get; set; } = string.Empty;
    public short    Capacidad     { get; set; }
    public decimal? DistanciaKm   { get; set; }
    public int?     TiempoMin     { get; set; }
    public string   Clasificacion { get; set; } = string.Empty;
}

public class OperadorSugeridoDto
{
    public int      Id            { get; set; }
    public string   Nombres       { get; set; } = string.Empty;
    public string   Apellidos     { get; set; } = string.Empty;
    public decimal? DistanciaKm   { get; set; }
    public int?     TiempoMin     { get; set; }
    public string   Clasificacion { get; set; } = string.Empty;
}

public class SugerenciasDto
{
    public List<GruaSugeridaDto>     Gruas      { get; set; } = [];
    public List<OperadorSugeridoDto> Operadores { get; set; } = [];
}
