namespace BackRomo.Application.DTOs.Flota;

public class DarDeBajaUnidadDto
{
    public int IdGrua         { get; set; }  // from route
    public int ActualizadoPor { get; set; }  // set from JWT in controller
}
