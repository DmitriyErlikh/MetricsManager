using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace MetricsManager.DAL
{
    public class DateTimeOffsetHandler : SqlMapper.TypeHandler<DateTimeOffset>
    {
        public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
            => parameter.Value = value;

        public override DateTimeOffset Parse(object value) => DateTimeOffset.FromUnixTimeSeconds((long)value);
    }
}
