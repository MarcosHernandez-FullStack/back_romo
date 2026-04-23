namespace BackRomo.Application.DTOs.Operador;

public class EditarOperadorDto
{
    public int      IdOperador   { get; set; }         // se asigna en el controller desde la ruta
    public string   Contrasena   { get; set; } = string.Empty;  // vacío = conservar actual
    public string   Nombres      { get; set; } = string.Empty;
    public string   Apellidos    { get; set; } = string.Empty;
    public string?  Telefono     { get; set; }
    public string   Correo       { get; set; } = string.Empty;
    public string   Rol          { get; set; } = "OPERADOR";
    public string   NroLicencia  { get; set; } = string.Empty;
    public DateOnly FecVenLic    { get; set; }
    public int      ActualizadoPor { get; set; }       // se asigna en el controller desde el JWT
}
