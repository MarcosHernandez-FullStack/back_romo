namespace BackRomo.Application.DTOs.Operador;

public class CrearOperadorDto
{
    public string   Alias        { get; set; } = string.Empty;
    public string   Contrasena   { get; set; } = string.Empty;  // plain text; se hashea en el repositorio
    public string   Nombres      { get; set; } = string.Empty;
    public string   Apellidos    { get; set; } = string.Empty;
    public string?  Telefono     { get; set; }
    public string   Correo       { get; set; } = string.Empty;
    public string   Rol          { get; set; } = "OPERADOR";
    public string   NroLicencia  { get; set; } = string.Empty;
    public DateOnly FecVenLic    { get; set; }
    public int      CreadoPor    { get; set; }  // se asigna desde el JWT en el controller
}
