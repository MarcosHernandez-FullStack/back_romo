using System.Data;
using Dapper;

namespace BackRomo.Infrastructure.Data;

public class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override DateOnly Parse(object value) => value switch
    {
        DateOnly d  => d,
        DateTime dt => DateOnly.FromDateTime(dt),
        _           => DateOnly.FromDateTime(Convert.ToDateTime(value)),
    };

    public override void SetValue(IDbDataParameter parameter, DateOnly value)
    {
        parameter.DbType = DbType.Date;
        parameter.Value  = value.ToDateTime(TimeOnly.MinValue);
    }
}

public class NullableDateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly?>
{
    public override DateOnly? Parse(object value) =>
        value == null || value is DBNull ? null : value switch
        {
            DateOnly d  => d,
            DateTime dt => DateOnly.FromDateTime(dt),
            _           => DateOnly.FromDateTime(Convert.ToDateTime(value)),
        };

    public override void SetValue(IDbDataParameter parameter, DateOnly? value)
    {
        if (value is null) { parameter.Value = DBNull.Value; return; }
        parameter.DbType = DbType.Date;
        parameter.Value  = value.Value.ToDateTime(TimeOnly.MinValue);
    }
}

public class TimeOnlyTypeHandler : SqlMapper.TypeHandler<TimeOnly>
{
    public override TimeOnly Parse(object value) => value switch
    {
        TimeOnly t  => t,
        TimeSpan ts => TimeOnly.FromTimeSpan(ts),
        _           => TimeOnly.FromTimeSpan((TimeSpan)value),
    };

    public override void SetValue(IDbDataParameter parameter, TimeOnly value)
    {
        parameter.DbType = DbType.Time;
        parameter.Value  = value.ToTimeSpan();
    }
}

public class NullableTimeOnlyTypeHandler : SqlMapper.TypeHandler<TimeOnly?>
{
    public override TimeOnly? Parse(object value) =>
        value == null || value is DBNull ? null : value switch
        {
            TimeOnly t  => t,
            TimeSpan ts => TimeOnly.FromTimeSpan(ts),
            _           => TimeOnly.FromTimeSpan((TimeSpan)value),
        };

    public override void SetValue(IDbDataParameter parameter, TimeOnly? value)
    {
        if (value is null) { parameter.Value = DBNull.Value; return; }
        parameter.DbType = DbType.Time;
        parameter.Value  = value.Value.ToTimeSpan();
    }
}
