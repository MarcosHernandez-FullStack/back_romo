using System.Data;
using BackRomo.Application.DTOs.Flota;
using BackRomo.Application.Interfaces;
using BackRomo.Infrastructure.Data;
using Dapper;

namespace BackRomo.Infrastructure.Repositories;

public class FlotaRepository : IFlotaRepository
{
    private readonly DbConnectionFactory _db;

    public FlotaRepository(DbConnectionFactory db)
    {
        _db = db;
    }

    public async Task<GruaPagedDto> ListarGruasAsync(string? estado, string? estadoOperacion, int? id, string? placa, string? marca, string? modelo, int? pagina, int? tamano, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();
        var param = new { Estado = estado, EstadoOperacion = estadoOperacion, Id = id, Placa = placa, Marca = marca, Modelo = modelo, Pagina = pagina, Tamano = tamano };

        using var multi = await conn.QueryMultipleAsync(new CommandDefinition(
            """
            SELECT * FROM fn_ListGruas(@Estado, @EstadoOperacion, @Id, @Placa, @Marca, @Modelo, @Pagina, @Tamano);
            SELECT
                COUNT(*)                                                                              AS Total,
                COUNT(*) FILTER (WHERE "EstadoOperacion" = 'OPERATIVA')                              AS Operativas,
                COUNT(*) FILTER (WHERE "EstadoOperacion" = 'ENTALLER')                               AS EnTaller,
                COUNT(*) FILTER (WHERE "FecVenSegRaw" <= CURRENT_DATE + INTERVAL '30 days')          AS SegurosCriticos
            FROM fn_ListGruas(@Estado, @EstadoOperacion, @Id, @Placa, @Marca, @Modelo, NULL, NULL);
            """,
            param,
            commandType: CommandType.Text,
            cancellationToken: ct
        ));

        var datos = (await multi.ReadAsync<UnidadDto>()).ToList();
        var (total, operativas, enTaller, segurosCriticos) = await multi.ReadFirstOrDefaultAsync<(long, long, long, long)>();

        return new GruaPagedDto
        {
            Total           = total,
            Operativas      = operativas,
            EnTaller        = enTaller,
            SegurosCriticos = segurosCriticos,
            Datos           = datos,
        };
    }

    public async Task<IEnumerable<BitaMantDto>> ListarBitaMantAsync(int idGrua, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();

        return await conn.QueryAsync<BitaMantDto>(new CommandDefinition(
            "SELECT * FROM fn_ListBitaMant(@IdGrua)",
            new { IdGrua = idGrua },
            commandType: CommandType.Text,
            cancellationToken: ct
        ));
    }

    public async Task<UnidadResultDto> CrearGruaAsync(CrearUnidadDto dto, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var p = new DynamicParameters();
            p.Add("_IdGrua",         0,                                           DbType.Int32);
            p.Add("_Placa",          dto.Placa,                                   DbType.String);
            p.Add("_Marca",          dto.Marca,                                   DbType.String);
            p.Add("_Modelo",         dto.Modelo,                                  DbType.String);
            p.Add("_AñoFabricacion", dto.AñoFabricacion,                          DbType.Int16);
            p.Add("_Capacidad",      dto.Capacidad,                               DbType.Int16);
            p.Add("_FecVenSeg",      dto.FecVenSeg.ToDateTime(TimeOnly.MinValue), DbType.Date);
            p.Add("_CreadoPor",      dto.CreadoPor,                               DbType.Int32);
            p.Add("_Exitoso", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
            p.Add("_Mensaje", value: "", dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);
            p.Add("_IdNuevo", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);

            await conn.ExecuteAsync(new CommandDefinition(
                "CALL sp_CreUpdGrua(@_IdGrua, @_Placa, @_Marca, @_Modelo, @_AñoFabricacion, @_Capacidad, @_FecVenSeg, @_CreadoPor, @_Exitoso, @_Mensaje, @_IdNuevo)",
                p, commandType: CommandType.Text, cancellationToken: ct
            ));

            return new UnidadResultDto
            {
                Exitoso = p.Get<int>("_Exitoso"),
                Mensaje = p.Get<string>("_Mensaje"),
                IdNuevo = p.Get<int>("_IdNuevo"),
            };
        }
        catch (OperationCanceledException)
        {
            return new UnidadResultDto { Exitoso = 2, Mensaje = "La operación tardó demasiado. Verifique si la grúa fue creada." };
        }
        catch (Exception ex)
        {
            return new UnidadResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
    }

    public async Task<UnidadResultDto> EditarGruaAsync(EditarUnidadDto dto, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var p = new DynamicParameters();
            p.Add("_IdGrua",         dto.IdGrua,                                  DbType.Int32);
            p.Add("_Placa",          dto.Placa,                                   DbType.String);
            p.Add("_Marca",          dto.Marca,                                   DbType.String);
            p.Add("_Modelo",         dto.Modelo,                                  DbType.String);
            p.Add("_AñoFabricacion", dto.AñoFabricacion,                          DbType.Int16);
            p.Add("_Capacidad",      dto.Capacidad,                               DbType.Int16);
            p.Add("_FecVenSeg",      dto.FecVenSeg.ToDateTime(TimeOnly.MinValue), DbType.Date);
            p.Add("_CreadoPor",      dto.ActualizadoPor,                          DbType.Int32);
            p.Add("_Exitoso", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
            p.Add("_Mensaje", value: "", dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);
            p.Add("_IdNuevo", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);

            await conn.ExecuteAsync(new CommandDefinition(
                "CALL sp_CreUpdGrua(@_IdGrua, @_Placa, @_Marca, @_Modelo, @_AñoFabricacion, @_Capacidad, @_FecVenSeg, @_CreadoPor, @_Exitoso, @_Mensaje, @_IdNuevo)",
                p, commandType: CommandType.Text, cancellationToken: ct
            ));

            return new UnidadResultDto
            {
                Exitoso = p.Get<int>("_Exitoso"),
                Mensaje = p.Get<string>("_Mensaje"),
                IdNuevo = p.Get<int>("_IdNuevo"),
            };
        }
        catch (OperationCanceledException)
        {
            return new UnidadResultDto { Exitoso = 2, Mensaje = "La operación tardó demasiado. Verifique si la grúa fue actualizada." };
        }
        catch (Exception ex)
        {
            return new UnidadResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
    }

    public async Task<UnidadResultDto> ActualizarEstadoAsync(UpdEstadoGruaDto dto, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var p = new DynamicParameters();
            p.Add("_IdGrua",         dto.IdGrua,         DbType.Int32);
            p.Add("_NuevoEstado",    dto.NuevoEstado,    DbType.String);
            p.Add("_ActualizadoPor", dto.ActualizadoPor, DbType.Int32);
            p.Add("_Exitoso", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
            p.Add("_Mensaje", value: "", dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);

            await conn.ExecuteAsync(new CommandDefinition(
                "CALL sp_UpdEstadoGrua(@_IdGrua, @_NuevoEstado, @_ActualizadoPor, @_Exitoso, @_Mensaje)",
                p, commandType: CommandType.Text, cancellationToken: ct
            ));

            return new UnidadResultDto
            {
                Exitoso = p.Get<int>("_Exitoso"),
                Mensaje = p.Get<string>("_Mensaje"),
            };
        }
        catch (OperationCanceledException)
        {
            return new UnidadResultDto { Exitoso = 2, Mensaje = "La operación tardó demasiado. Verifique si el estado de la grúa fue actualizado." };
        }
        catch (Exception ex)
        {
            return new UnidadResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
    }

    public async Task<IEnumerable<ReservaALiberarDto>> ListarReservasALiberarAsync(int idGrua, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();

        return await conn.QueryAsync<ReservaALiberarDto>(new CommandDefinition(
            "SELECT * FROM fn_ListReservasALiberar(@IdGrua)",
            new { IdGrua = idGrua },
            commandType: CommandType.Text,
            cancellationToken: ct
        ));
    }

    public async Task<UnidadResultDto> IngresoTallerAsync(IngresoTallerDto dto, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var p = new DynamicParameters();
            p.Add("_IdGrua",            dto.IdGrua,            DbType.Int32);
            p.Add("_NombreResponsable", dto.NombreResponsable, DbType.String);
            p.Add("_Kilometraje",       dto.Kilometraje,       DbType.Int32);
            p.Add("_Nota",              dto.Nota,              DbType.String);
            p.Add("_ActualizadoPor",    dto.ActualizadoPor,    DbType.Int32);
            p.Add("_Exitoso", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
            p.Add("_Mensaje", value: "", dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);

            await conn.ExecuteAsync(new CommandDefinition(
                "CALL sp_IngresoTaller(@_IdGrua, @_NombreResponsable, @_Kilometraje, @_Nota, @_ActualizadoPor, @_Exitoso, @_Mensaje)",
                p, commandType: CommandType.Text, cancellationToken: ct
            ));

            return new UnidadResultDto
            {
                Exitoso = p.Get<int>("_Exitoso"),
                Mensaje = p.Get<string>("_Mensaje"),
            };
        }
        catch (OperationCanceledException)
        {
            return new UnidadResultDto { Exitoso = 2, Mensaje = "La operación tardó demasiado. Verifique si la grúa fue enviada a taller." };
        }
        catch (Exception ex)
        {
            return new UnidadResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
    }

    public async Task<UnidadResultDto> RetornoOperativaAsync(RetornoOperativaDto dto, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var p = new DynamicParameters();
            p.Add("_IdGrua",            dto.IdGrua,            DbType.Int32);
            p.Add("_NombreResponsable", dto.NombreResponsable, DbType.String);
            p.Add("_Kilometraje",       dto.Kilometraje,       DbType.Int32);
            p.Add("_Nota",              dto.Nota,              DbType.String);
            p.Add("_ActualizadoPor",    dto.ActualizadoPor,    DbType.Int32);
            p.Add("_Exitoso", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
            p.Add("_Mensaje", value: "", dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);

            await conn.ExecuteAsync(new CommandDefinition(
                "CALL sp_RetornoOperativa(@_IdGrua, @_NombreResponsable, @_Kilometraje, @_Nota, @_ActualizadoPor, @_Exitoso, @_Mensaje)",
                p, commandType: CommandType.Text, cancellationToken: ct
            ));

            return new UnidadResultDto
            {
                Exitoso = p.Get<int>("_Exitoso"),
                Mensaje = p.Get<string>("_Mensaje"),
            };
        }
        catch (OperationCanceledException)
        {
            return new UnidadResultDto { Exitoso = 2, Mensaje = "La operación tardó demasiado. Verifique si la grúa fue retornada a operativa." };
        }
        catch (Exception ex)
        {
            return new UnidadResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
    }
}
