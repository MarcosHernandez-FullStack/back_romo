using BackRomo.Application.DTOs.Auth;
using BackRomo.Application.Interfaces;

namespace BackRomo.Application.Services;

public class AuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IJwtService _jwtService;

    public AuthService(IAuthRepository authRepository, IJwtService jwtService)
    {
        _authRepository = authRepository;
        _jwtService     = jwtService;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
    {
        var usuario = await _authRepository.LoginAsync(request.Identificador, request.Contrasena);

        if (usuario is null)
            return null;

        var token = _jwtService.GenerarToken(usuario);

        return new LoginResponseDto
        {
            Token     = token,
            Id        = usuario.Id,
            Alias     = usuario.Alias,
            Nombres   = usuario.Nombres,
            Apellidos = usuario.Apellidos,
            Correo    = usuario.Correo,
            Telefono  = usuario.Telefono,
            Rol       = usuario.Rol
        };
    }
}
