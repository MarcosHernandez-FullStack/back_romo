using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BackRomo.Application.Interfaces;
using BackRomo.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BackRomo.Infrastructure.Auth;

public class JwtService : IJwtService
{
    private readonly string _key;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expiresInMinutes;

    public JwtService(IConfiguration config)
    {
        _key              = config["Jwt:Key"]!;
        _issuer           = config["Jwt:Issuer"]!;
        _audience         = config["Jwt:Audience"]!;
        _expiresInMinutes = int.Parse(config["Jwt:ExpiresInMinutes"]!);
    }

    public (string Token, DateTime ExpiresAt) GenerarToken(Usuario usuario)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name,           usuario.Alias),
            new Claim(ClaimTypes.Email,          usuario.Correo),
            new Claim(ClaimTypes.Role,           usuario.Rol)
        };

        var key         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiresAt   = DateTime.UtcNow.AddMinutes(_expiresInMinutes);

        var token = new JwtSecurityToken(
            issuer:             _issuer,
            audience:           _audience,
            claims:             claims,
            expires:            expiresAt,
            signingCredentials: credentials
        );

        return (new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
    }
}
