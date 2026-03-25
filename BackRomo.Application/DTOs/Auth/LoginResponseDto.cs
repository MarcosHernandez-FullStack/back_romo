namespace BackRomo.Application.DTOs.Auth;

public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public int Id { get; set; }
    public string Alias { get; set; } = string.Empty;
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string Rol { get; set; } = string.Empty;
    public int? IdCliente { get; set; }
    public decimal? TarifaKm { get; set; }
    public decimal? TarifaBase { get; set; }
    public string? Empresa { get; set; }
}
