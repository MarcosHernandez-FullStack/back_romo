namespace BackRomo.Application.DTOs.Configuracion;

public class TarifaDto
{
    public decimal TarifaBase { get; set; }
    public decimal TarifaKm   { get; set; }
    public string  Estado     { get; set; } = string.Empty;
}
