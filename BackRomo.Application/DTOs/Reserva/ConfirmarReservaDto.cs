namespace BackRomo.Application.DTOs.Reserva;

public class VehiculoDetalleDto
{
    public string Tipo        { get; set; } = string.Empty;
    public string Placa       { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Observacion { get; set; } = string.Empty;
}

public class ConfirmarReservaDto
{
    public int                       IdTimerReserva { get; set; }
    public int                       ActualizadoPor { get; set; }
    public List<VehiculoDetalleDto>  Vehiculos      { get; set; } = [];
}
