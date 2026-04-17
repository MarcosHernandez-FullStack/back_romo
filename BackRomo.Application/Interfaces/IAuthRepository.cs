using BackRomo.Domain.Entities;

namespace BackRomo.Application.Interfaces;

public interface IAuthRepository
{
    Task<(Usuario? Usuario, string Mensaje)> LoginAsync(string identificador, string contrasena, CancellationToken ct = default);
}
