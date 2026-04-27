namespace BackRomo.Application.DTOs.Flota;

public class UpdEstadoGruaDto
{
    public int    IdGrua         { get; set; }  // from route
    public string NuevoEstado    { get; set; } = string.Empty;
    public int    ActualizadoPor { get; set; }  // set from JWT in controller
}
