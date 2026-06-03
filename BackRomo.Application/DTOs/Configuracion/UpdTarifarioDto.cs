using System.ComponentModel.DataAnnotations;

namespace BackRomo.Application.DTOs.Configuracion;

public class UpdTarifarioDto
{
    public int Id { get; set; }

    [Range(typeof(decimal), "0.01", "99999999.99",
        ErrorMessage = "La tarifa base debe ser mayor a 0.")]
    public decimal TarifaBase { get; set; }

    [Range(typeof(decimal), "0.01", "99999999.99",
        ErrorMessage = "La tarifa por km debe ser mayor a 0.")]
    public decimal TarifaKm { get; set; }
}
