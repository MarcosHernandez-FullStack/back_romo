namespace BackRomo.Application.DTOs.Flota;

public class IngresoTallerDto
{
    public int     IdGrua            { get; set; }  // from route
    public string  NombreResponsable { get; set; } = string.Empty;
    public int     Kilometraje       { get; set; }
    public string? Nota              { get; set; }
    public int     ActualizadoPor    { get; set; }  // set from JWT in controller
}
