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

    public async Task<ReportePagedDto> ListarReportesAsync(
        int?    id,
        int?    idCliente,
        string? fechaDesde,
        string? fechaHasta,
        string? estadoOperacion,
        string? estadoAdministrativo,
        string? placa,
        string? empresa,
        int?    pagina,
        int?    tamano,
        CancellationToken ct = default)
    {
        using var conn  = _db.CreateConnection();
        var       param = new
        {
            Id                   = id,
            IdCliente            = idCliente,
            FechaDesde           = string.IsNullOrWhiteSpace(fechaDesde)           ? null : fechaDesde,
            FechaHasta           = string.IsNullOrWhiteSpace(fechaHasta)           ? null : fechaHasta,
            EstadoOperacion      = string.IsNullOrWhiteSpace(estadoOperacion)      ? null : estadoOperacion,
            EstadoAdministrativo = string.IsNullOrWhiteSpace(estadoAdministrativo) ? null : estadoAdministrativo,
            Placa                = string.IsNullOrWhiteSpace(placa)                ? null : placa,
            Empresa              = string.IsNullOrWhiteSpace(empresa)              ? null : empresa,
            Pagina               = pagina,
            Tamano               = tamano,
        };

        using var multi = await conn.QueryMultipleAsync(new CommandDefinition(
            """
            SELECT * FROM fn_ReporteServicios(@Id, @IdCliente, @FechaDesde::date, @FechaHasta::date, @EstadoOperacion, @EstadoAdministrativo, @Placa, @Empresa, @Pagina, @Tamano);
            SELECT
                COUNT(*)                                                                                          AS Total,
                COUNT(*) FILTER (WHERE "Estado" = 'Finalizado')                                                  AS Finalizados,
                COUNT(*) FILTER (WHERE "Estado" = 'Cancelado')                                                   AS Cancelados,
                COALESCE(SUM(CASE WHEN "Estado" = 'Finalizado' THEN "Costo" ELSE 0 END), 0)                     AS MontoTotal,
                COUNT(*) FILTER (WHERE "EstadoAdministrativo" = 'Pendiente' AND "Estado" = 'Finalizado')        AS Pendientes,
                COUNT(*) FILTER (WHERE "EstadoAdministrativo" = 'Facturado')                                     AS Facturados,
                COUNT(*) FILTER (WHERE "EstadoAdministrativo" = 'Pagado')                                        AS Pagados
            FROM fn_ReporteServicios(@Id, @IdCliente, @FechaDesde::date, @FechaHasta::date, @EstadoOperacion, @EstadoAdministrativo, @Placa, @Empresa, NULL, NULL);
            """,
            param,
            commandType: CommandType.Text,
            cancellationToken: ct
        ));

        var datos = (await multi.ReadAsync<ReporteDto>()).ToList();
        var (total, finalizados, cancelados, montoTotal, pendientes, facturados, pagados) =
            await multi.ReadFirstOrDefaultAsync<(long, long, long, decimal, long, long, long)>();

        return new ReportePagedDto
        {
            Total       = total,
            Finalizados = finalizados,
            Cancelados  = cancelados,
            MontoTotal  = montoTotal,
            Pendientes  = pendientes,
            Facturados  = facturados,
            Pagados     = pagados,
            Datos       = datos,
        };
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
