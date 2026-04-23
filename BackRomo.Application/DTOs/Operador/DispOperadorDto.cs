namespace BackRomo.Application.DTOs.Operador;

public class DispOperadorDto
{
    public List<DispSlotDto> Slots               { get; set; } = new();
    public int               TotalHorasSemanales { get; set; }
    public int               DiasActivos         { get; set; }
}

public class DispSlotDto
{
    public int    Id         { get; set; }
    public short  NroDia     { get; set; }
    public string NombreDia  { get; set; } = string.Empty;
    public string HoraInicio { get; set; } = string.Empty;
    public string HoraFin    { get; set; } = string.Empty;
}
