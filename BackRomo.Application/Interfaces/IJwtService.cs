using BackRomo.Domain.Entities;

namespace BackRomo.Application.Interfaces;

public interface IJwtService
{
    string GenerarToken(Usuario usuario);
}
