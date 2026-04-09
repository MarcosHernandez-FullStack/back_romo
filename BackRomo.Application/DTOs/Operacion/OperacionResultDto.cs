namespace BackRomo.Application.DTOs.Operacion;

public class OperacionResultDto
{
    public int     Exitoso         { get; set; }
    public string  Mensaje         { get; set; } = string.Empty;
    public string? HorasConflicto  { get; set; }
}
