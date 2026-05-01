using BackRomo.Application.DTOs.Usuario;

namespace BackRomo.Application.Interfaces;

public interface IUsuarioRepository
{
    Task<IEnumerable<UsuarioDto>> ListarUsuariosAsync(string? estado, int? id, string? rol, CancellationToken ct = default);
    Task<UsuarioResultDto>        CrearUsuarioAsync   (CrearUsuarioDto  dto,                CancellationToken ct = default);
    Task<UsuarioResultDto>        EditarUsuarioAsync  (EditarUsuarioDto dto,                CancellationToken ct = default);
}
