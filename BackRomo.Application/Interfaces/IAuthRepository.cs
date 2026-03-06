using BackRomo.Domain.Entities;

namespace BackRomo.Application.Interfaces;

public interface IAuthRepository
{
    Task<Usuario?> LoginAsync(string identificador, string contrasena);
}
