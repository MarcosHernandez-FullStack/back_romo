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

    public async Task<UsuarioPagedDto> ListarUsuariosAsync(string? estado, int? id, string? nombre, string? correo, string? rol, int? pagina, int? tamano, CancellationToken ct = default)
        => await _usuarioRepository.ListarUsuariosAsync(estado, id, nombre, correo, rol, pagina, tamano, ct);

    public async Task<UsuarioResultDto> CrearUsuarioAsync(CrearUsuarioDto dto, CancellationToken ct = default)
        => await _usuarioRepository.CrearUsuarioAsync(dto, ct);

    public async Task<UsuarioResultDto> EditarUsuarioAsync(EditarUsuarioDto dto, CancellationToken ct = default)
        => await _usuarioRepository.EditarUsuarioAsync(dto, ct);

    public async Task<UsuarioResultDto> ActualizarEstadoUsuarioAsync(UpdEstadoUsuarioDto dto, CancellationToken ct = default)
        => await _usuarioRepository.ActualizarEstadoUsuarioAsync(dto, ct);
}
