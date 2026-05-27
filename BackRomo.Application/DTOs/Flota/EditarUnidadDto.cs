using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackRomo.Application.DTOs.Flota;

public class EditarUnidadDto
{
    public int      IdGrua         { get; set; }  // se asigna en el controller desde la ruta

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
    public short    Capacidad      { get; set; }
    public DateOnly FecVenSeg      { get; set; }
    public int      ActualizadoPor { get; set; }  // se asigna desde el JWT en el controller
}
