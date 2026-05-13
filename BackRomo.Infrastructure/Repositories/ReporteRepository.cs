using System.Data;
using BackRomo.Application.DTOs.Reporte;
using BackRomo.Application.Interfaces;
using BackRomo.Infrastructure.Data;
using Dapper;

namespace BackRomo.Infrastructure.Repositories;

public class ReporteRepository : IReporteRepository
{
    private readonly DbConnectionFactory _db;

    public ReporteRepository(DbConnectionFactory db)
    {
        _db = db;
    }

    public async Task<IEnumerable<ReporteDto>> ListarReportesAsync(
        string? busqueda,
        int?    idCliente,
        string? fechaDesde,
        string? fechaHasta,
        string? estadoOperacion,
        string? estadoAdministrativo,
        CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();

        return await conn.QueryAsync<ReporteDto>(new CommandDefinition(
            "SELECT * FROM fn_ReporteServicios(@_Busqueda, @_IdCliente, @_FechaDesde::date, @_FechaHasta::date, @_EstadoOperacion, @_EstadoAdministrativo)",
            new
            {
                _Busqueda             = string.IsNullOrWhiteSpace(busqueda)             ? null : busqueda,
                _IdCliente            = idCliente,
                _FechaDesde           = string.IsNullOrWhiteSpace(fechaDesde)           ? null : fechaDesde,
                _FechaHasta           = string.IsNullOrWhiteSpace(fechaHasta)           ? null : fechaHasta,
                _EstadoOperacion      = string.IsNullOrWhiteSpace(estadoOperacion)      ? null : estadoOperacion,
                _EstadoAdministrativo = string.IsNullOrWhiteSpace(estadoAdministrativo) ? null : estadoAdministrativo,
            },
            commandType: CommandType.Text,
            cancellationToken: ct
        ));
    }

    public async Task<ReporteResultDto> UpdEstadoAdministrativoAsync(UpdEstadoAdministrativoDto dto, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var p = new DynamicParameters();
            p.Add("_EstadoAdministrativo", dto.EstadoAdministrativo, DbType.String);
            p.Add("_IdReserva",            dto.IdReserva,            DbType.Int32);
            p.Add("_ActualizadoPor",       dto.ActualizadoPor,       DbType.Int32);
            p.Add("_Exitoso", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
            p.Add("_Mensaje", value: "", dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);

            await conn.ExecuteAsync(new CommandDefinition(
                "CALL sp_UpdEstadoAdministrativo(@_EstadoAdministrativo, @_IdReserva, @_ActualizadoPor, @_Exitoso, @_Mensaje)",
                p, commandType: CommandType.Text, cancellationToken: ct
            ));

            return new ReporteResultDto { Exitoso = p.Get<int>("_Exitoso"), Mensaje = p.Get<string>("_Mensaje") };
        }
        catch (OperationCanceledException)
        {
            return new ReporteResultDto { Exitoso = 2, Mensaje = "La operación tardó demasiado. Verifique si el estado fue actualizado." };
        }
        catch (Exception ex)
        {
            return new ReporteResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
    }
}
