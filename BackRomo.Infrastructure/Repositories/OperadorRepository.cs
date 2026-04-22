using BackRomo.Application.DTOs.Operador;
using BackRomo.Application.Interfaces;
using BackRomo.Infrastructure.Data;
using Dapper;

namespace BackRomo.Infrastructure.Repositories;

public class OperadorRepository : IOperadorRepository
{
    private readonly DbConnectionFactory _db;

    public OperadorRepository(DbConnectionFactory db)
    {
        _db = db;
    }

    public async Task<IEnumerable<OperadorListDto>> ListarOperadoresAsync(string? estado, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();

        var rows = await conn.QueryAsync<OperadorDapperRow>(new CommandDefinition(
            "SELECT * FROM fn_ListOperadores(@Estado)",
            new { Estado = estado },
            cancellationToken: ct
        ));

        return rows.Select(r => new OperadorListDto
        {
            Id                      = r.Id,
            Alias                   = r.Alias,
            NombresCompleto         = r.NombresCompleto,
            Telefono                = r.Telefono,
            NroLicencia             = r.NroLicencia,
            FecVenLic               = DateOnly.FromDateTime(r.FecVenLic),
            Estado                  = r.Estado,
            ProximaFechaServicio    = r.ProximaFechaServicio.HasValue
                                        ? DateOnly.FromDateTime(r.ProximaFechaServicio.Value)
                                        : null,
            ProximaHoraServicio     = r.ProximaHoraServicio.HasValue
                                        ? TimeOnly.FromTimeSpan(r.ProximaHoraServicio.Value)
                                        : null,
            TotalServiciosAsignados = r.TotalServiciosAsignados,
            TotalHorasSemanales     = r.TotalHorasSemanales,
        });
    }

    private class OperadorDapperRow
    {
        public int      Id                      { get; set; }
        public string   Alias                   { get; set; } = string.Empty;
        public string   NombresCompleto         { get; set; } = string.Empty;
        public string?  Telefono                { get; set; }
        public string   NroLicencia             { get; set; } = string.Empty;
        public DateTime FecVenLic               { get; set; }
        public string   Estado                  { get; set; } = string.Empty;
        public DateTime? ProximaFechaServicio   { get; set; }
        public TimeSpan? ProximaHoraServicio    { get; set; }
        public int      TotalServiciosAsignados { get; set; }
        public int      TotalHorasSemanales     { get; set; }
    }
}
