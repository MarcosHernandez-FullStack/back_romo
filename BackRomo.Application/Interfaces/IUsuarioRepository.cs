using BackRomo.Application.DTOs.Usuario;

namespace BackRomo.Application.Interfaces;

public interface IUsuarioRepository
{
    Task<UsuarioPagedDto> ListarUsuariosAsync(string? estado, int? id, string? nombre, string? correo, string? rol, int? pagina, int? tamano, CancellationToken ct = default);
    Task<UsuarioResultDto>        CrearUsuarioAsync          (CrearUsuarioDto             dto, CancellationToken ct = default);
    Task<UsuarioResultDto>        EditarUsuarioAsync         (EditarUsuarioDto            dto, CancellationToken ct = default);
    Task<UsuarioResultDto>        ActualizarEstadoUsuarioAsync(UpdEstadoUsuarioDto         dto, CancellationToken ct = default);
}
