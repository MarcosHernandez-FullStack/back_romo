using BackRomo.Application.DTOs.Auth;
using BackRomo.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace BackRomo.Application.Services;

public class AuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IJwtService _jwtService;
    private readonly ILogger<AuthService> _logger;

    public AuthService(IAuthRepository authRepository, IJwtService jwtService, ILogger<AuthService> logger)
    {
        _authRepository = authRepository;
        _jwtService     = jwtService;
        _logger         = logger;
    }

    public async Task<(LoginResponseDto? Response, string Mensaje)> LoginAsync(LoginRequestDto request, CancellationToken ct = default)
    {
        _logger.LogInformation("Intento de login para identificador {Identificador}", request.Identificador);

        var (usuario, mensaje) = await _authRepository.LoginAsync(request.Identificador, request.Contrasena, ct);

        if (usuario is null)
        {
            _logger.LogWarning("Login fallido para identificador {Identificador}: {Mensaje}", request.Identificador, mensaje);
            return (null, mensaje);
        }

        var (token, expiresAt) = _jwtService.GenerarToken(usuario);

        _logger.LogInformation("Login exitoso para usuario {UserId} ({Rol}), token expira {ExpiresAt}",
            usuario.Id, usuario.Rol, expiresAt);

        return (new LoginResponseDto
        {
            Token      = token,
            ExpiresAt  = expiresAt,
            Id         = usuario.Id,
            Alias      = usuario.Alias,
            Nombres    = usuario.Nombres,
            Apellidos  = usuario.Apellidos,
            Correo     = usuario.Correo,
            Telefono   = usuario.Telefono,
            Rol        = usuario.Rol,
            IdCliente  = usuario.IdCliente,
            TarifaKm   = usuario.TarifaKm,
            TarifaBase = usuario.TarifaBase,
            Empresa    = usuario.Empresa,
            IdOperador = usuario.IdOperador,
            NroLicencia          = usuario.NroLicencia,
            FecVenLic            = usuario.FecVenLic,
            ServiciosCompletados = usuario.ServiciosCompletados,
        }, string.Empty);
    }
}
