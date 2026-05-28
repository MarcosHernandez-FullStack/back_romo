using System.ComponentModel.DataAnnotations;

namespace BackRomo.Application.DTOs.Reserva;

public class VehiculoDetalleDto
{
    [Required(ErrorMessage = "El tipo de vehículo es obligatorio.")]
    [MaxLength(100, ErrorMessage = "El tipo de vehículo no puede superar los 100 caracteres.")]
    public string Tipo        { get; set; } = string.Empty;

    [MaxLength(10, ErrorMessage = "La placa no puede superar los 10 caracteres.")]
    public string Placa       { get; set; } = string.Empty;

    [MaxLength(200, ErrorMessage = "La descripción no puede superar los 200 caracteres.")]
    public string Descripcion { get; set; } = string.Empty;

    [MaxLength(100, ErrorMessage = "La observación no puede superar los 100 caracteres.")]
    public string Observacion { get; set; } = string.Empty;
}

public class ConfirmarReservaDto
{
    public int                      IdTimerReserva { get; set; }
    public string                   Rol            { get; set; } = string.Empty;

    [Required(ErrorMessage = "La lista de vehículos es obligatoria.")]
    public List<VehiculoDetalleDto> Vehiculos      { get; set; } = [];
}
