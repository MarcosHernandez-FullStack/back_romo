using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackRomo.Application.DTOs.Flota;

public class CrearUnidadDto
{
    [Required(ErrorMessage = "La placa es obligatoria.")]
    [MaxLength(10, ErrorMessage = "La placa no puede superar los 10 caracteres.")]
    public string   Placa          { get; set; } = string.Empty;

    [Required(ErrorMessage = "La marca es obligatoria.")]
    [MaxLength(30, ErrorMessage = "La marca no puede superar los 30 caracteres.")]
    public string   Marca          { get; set; } = string.Empty;

    [Required(ErrorMessage = "El modelo es obligatorio.")]
    [MaxLength(30, ErrorMessage = "El modelo no puede superar los 30 caracteres.")]
    public string   Modelo         { get; set; } = string.Empty;
    [JsonPropertyName("anioFabricacion")]
    public short    AñoFabricacion { get; set; }
    [Range(1, short.MaxValue, ErrorMessage = "La capacidad debe ser mayor o igual a 1.")]
    public short    Capacidad      { get; set; }
    public DateOnly FecVenSeg      { get; set; }
    public int      CreadoPor      { get; set; }  // se asigna desde el JWT en el controller
}
