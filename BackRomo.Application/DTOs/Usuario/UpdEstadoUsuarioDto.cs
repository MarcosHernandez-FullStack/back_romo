namespace BackRomo.Application.DTOs.Usuario;

public class UpdEstadoUsuarioDto
{
    public int    IdUsuario      { get; set; }
    public string NuevoEstado    { get; set; } = string.Empty;
    public int    ActualizadoPor { get; set; }
}
