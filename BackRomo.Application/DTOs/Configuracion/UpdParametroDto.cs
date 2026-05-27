using System.ComponentModel.DataAnnotations;

namespace BackRomo.Application.DTOs.Configuracion;

public class UpdParametroDto
{
    public int     Id                   { get; set; }  // se asigna en el controller desde la ruta
    public short   TiempoMargenManiobra { get; set; }
    public short   TiempoRetornoBase    { get; set; }
    public decimal UmbralLargaDistancia { get; set; }
    public short   TiempoTolerancia     { get; set; }
    public short   TiempoCorte          { get; set; }
    public short   TimerAdministrativo  { get; set; }
    public short   TimerCliente         { get; set; }

    [Required(ErrorMessage = "La zona horaria es obligatoria.")]
    [MaxLength(50, ErrorMessage = "La zona horaria no puede superar los 50 caracteres.")]
    public string  ZonaHoraria          { get; set; } = string.Empty;

    public short   MinutosCerca         { get; set; }
    public short   MinutosMedio         { get; set; }

    [Required(ErrorMessage = "La coordenada de latitud es obligatoria.")]
    [MaxLength(20, ErrorMessage = "La coordenada de latitud no puede superar los 20 caracteres.")]
    public string  CoordLatMaps         { get; set; } = string.Empty;

    [Required(ErrorMessage = "La coordenada de longitud es obligatoria.")]
    [MaxLength(20, ErrorMessage = "La coordenada de longitud no puede superar los 20 caracteres.")]
    public string  CoordLonMaps         { get; set; } = string.Empty;

    public decimal MetrosCercania       { get; set; }
}
