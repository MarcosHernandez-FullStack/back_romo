namespace BackRomo.Application.DTOs.Auth;

public class LoginRequestDto
{
    public string Identificador { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
}
