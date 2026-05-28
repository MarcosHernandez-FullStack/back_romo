using System.ComponentModel.DataAnnotations;

namespace BackRomo.Application.DTOs.Reserva;

public class CrearReservaDto
{
    public DateOnly FechaServicio { get; set; }
    public TimeOnly HoraInicio    { get; set; }
    public TimeOnly HoraFin       { get; set; }

    [Range(1, short.MaxValue, ErrorMessage = "La cantidad de carga debe ser mayor o igual a 1.")]
    public short    CantidadCarga { get; set; }

    public string   Rol           { get; set; } = string.Empty;
    public int      IdCliente     { get; set; }
    public int?     IdOperador    { get; set; }

    [Required(ErrorMessage = "La dirección de origen es obligatoria.")]
    public string   DireccionOrigen  { get; set; } = string.Empty;

    [Required(ErrorMessage = "La coordenada de latitud de origen es obligatoria.")]
    [MaxLength(20,  ErrorMessage = "La coordenada de latitud de origen no puede superar los 20 caracteres.")]
    public string   CoordLatOrigen   { get; set; } = string.Empty;

    [Required(ErrorMessage = "La coordenada de longitud de origen es obligatoria.")]
    [MaxLength(20,  ErrorMessage = "La coordenada de longitud de origen no puede superar los 20 caracteres.")]
    public string   CoordLonOrigen   { get; set; } = string.Empty;

    [Required(ErrorMessage = "La dirección de destino es obligatoria.")]
    public string   DireccionDestino { get; set; } = string.Empty;

    [Required(ErrorMessage = "La coordenada de latitud de destino es obligatoria.")]
    [MaxLength(20,  ErrorMessage = "La coordenada de latitud de destino no puede superar los 20 caracteres.")]
    public string   CoordLatDestino  { get; set; } = string.Empty;

    [Required(ErrorMessage = "La coordenada de longitud de destino es obligatoria.")]
    [MaxLength(20,  ErrorMessage = "La coordenada de longitud de destino no puede superar los 20 caracteres.")]
    public string   CoordLonDestino  { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "La distancia debe ser mayor a 0.")]
    public decimal  DistanciaKm      { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "El tiempo estimado debe ser mayor o igual a 1.")]
    public int      TiempoEstimado   { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "El tiempo de maniobra debe ser mayor o igual a 1.")]
    public int      TiempoManiobra   { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "El tiempo de retorno no puede ser negativo.")]
    public int      TiempoRetorno    { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "El número de bloques debe ser mayor o igual a 1.")]
    public int      NroBloques       { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "El costo por km no puede ser negativo.")]
    public decimal  CostoKm          { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "El costo base no puede ser negativo.")]
    public decimal  CostoBase        { get; set; }

    [Range(1, short.MaxValue, ErrorMessage = "El tiempo de expiración del timer debe ser mayor o igual a 1.")]
    public short    TimerExpiracion      { get; set; }

    public int      CreadoPor            { get; set; }

    [Required(ErrorMessage = "El tipo de horario es obligatorio.")]
    [MaxLength(50,  ErrorMessage = "El tipo de horario no puede superar los 50 caracteres.")]
    public string   TipoHorario          { get; set; } = string.Empty;

    [Required(ErrorMessage = "El estado de operación es obligatorio.")]
    [MaxLength(50, ErrorMessage = "El estado de operación no puede superar los 50 caracteres.")]
    [RegularExpression(@"^(RESERVADO|ASIGNADO|ENCURSO|FINALIZADO|CANCELADO)$", ErrorMessage = "El estado de operación no es válido.")]
    public string   EstadoOperacion      { get; set; } = string.Empty;

    [Required(ErrorMessage = "El estado administrativo es obligatorio.")]
    [MaxLength(50, ErrorMessage = "El estado administrativo no puede superar los 50 caracteres.")]
    [RegularExpression(@"^(PENDIENTE|FACTURADO|PAGADO)$", ErrorMessage = "El estado administrativo no es válido.")]
    public string   EstadoAdministrativo { get; set; } = string.Empty;

    [Required(ErrorMessage = "El estado es obligatorio.")]
    [MaxLength(10, ErrorMessage = "El estado no puede superar los 10 caracteres.")]
    [RegularExpression(@"^(ACTIVO|INACTIVO)$", ErrorMessage = "El estado no es válido.")]
    public string   Estado               { get; set; } = string.Empty;

    public DateTime FechaCreacion        { get; set; }
}
