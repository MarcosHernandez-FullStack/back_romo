namespace BackRomo.Application.DTOs.Configuracion;

public class UpdTarifarioDto
{
    public int     Id         { get; set; }
    public decimal TarifaBase { get; set; }
    public decimal TarifaKm   { get; set; }
}
