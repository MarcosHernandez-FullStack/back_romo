namespace BackRomo.Application.DTOs.Usuario;

public class UsuarioPagedDto
{
    public long                    Total                { get; set; }
    public long                    TotalAdministradores { get; set; }
    public long                    TotalStaff           { get; set; }
    public IEnumerable<UsuarioDto> Datos                { get; set; } = [];
}
