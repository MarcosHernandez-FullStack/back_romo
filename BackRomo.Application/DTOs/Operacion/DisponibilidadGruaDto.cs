using System.Text.Json.Serialization;

namespace BackRomo.Application.DTOs.Operacion;

public class DisponibilidadGruaDto
{
    [JsonIgnore]
    public TimeSpan Hora                    { get; set; }

    /// <summary>Hora formateada "HH:mm" para el frontend.</summary>
    public string   HoraStr                 => Hora.ToString(@"hh\:mm");

    public short    Capacidad               { get; set; }
    public int      CantidadReservas        { get; set; }
    public int      CantidadGruas           { get; set; }
    public int      CantidadGruasDisponible { get; set; }
}
