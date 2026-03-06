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
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaActualizacion { get; set; }
    public int CreadoPor { get; set; }
    public int? ActualizadoPor { get; set; }
}
