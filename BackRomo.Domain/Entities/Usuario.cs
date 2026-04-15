namespace BackRomo.Domain.Entities;

public class Usuario
{
    public int Id { get; set; }
    public string Alias { get; set; } = string.Empty;
    public string Contraseña { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Rol { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public int? IdCliente  { get; set; }
    public int? IdOperador { get; set; }
    public decimal? TarifaKm { get; set; }
    public decimal? TarifaBase { get; set; }
    public string? Empresa { get; set; }
    public string? NroLicencia { get; set; }
    public DateOnly? FecVenLic { get; set; }
    public int? ServiciosCompletados { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaActualizacion { get; set; }
    public int CreadoPor { get; set; }
    public int? ActualizadoPor { get; set; }
}
