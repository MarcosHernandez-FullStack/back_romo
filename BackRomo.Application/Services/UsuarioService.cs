using BackRomo.Application.DTOs.Usuario;
using BackRomo.Application.Interfaces;

namespace BackRomo.Application.Services;

public class UsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IEnumerable<UsuarioDto>> ListarUsuariosAsync(string? estado, int? id, string? rol, CancellationToken ct = default)
        => await _usuarioRepository.ListarUsuariosAsync(estado, id, rol, ct);

    public async Task<UsuarioResultDto> CrearUsuarioAsync(CrearUsuarioDto dto, CancellationToken ct = default)
        => await _usuarioRepository.CrearUsuarioAsync(dto, ct);

    public async Task<UsuarioResultDto> EditarUsuarioAsync(EditarUsuarioDto dto, CancellationToken ct = default)
        => await _usuarioRepository.EditarUsuarioAsync(dto, ct);
}
