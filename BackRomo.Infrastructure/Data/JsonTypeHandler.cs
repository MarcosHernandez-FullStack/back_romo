using System.Data;
using System.Text.Json;
using Dapper;

namespace BackRomo.Infrastructure.Data;

public class JsonTypeHandler<T> : SqlMapper.TypeHandler<T>
{
    private static readonly JsonSerializerOptions _opts =
        new() { PropertyNameCaseInsensitive = true };

    public override T? Parse(object value)
        => value is string s ? JsonSerializer.Deserialize<T>(s, _opts) : default;

    public override void SetValue(IDbDataParameter parameter, T? value)
        => parameter.Value = JsonSerializer.Serialize(value, _opts);
}
