using BackRomo.Domain.Entities;

namespace BackRomo.Application.Interfaces;

public interface IJwtService
{
    (string Token, DateTime ExpiresAt) GenerarToken(Usuario usuario);
}
