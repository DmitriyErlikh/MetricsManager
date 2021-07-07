using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MetricsAgent.DAL;
using Dapper;
using System.Linq;

namespace MetricsAgent.DAL
{
    public interface IHDDMetricsRepository : IRepository<HDDMetric>
    {

    }

    public class HDDMetricsRepository : IHDDMetricsRepository
    {
        IConnectionManager _connection;
        public HDDMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(HDDMetric item)
        {
            using var connection = _connection.CreateOpenedConnection();

            connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds()
                });
        }

        public IList<HDDMetric> GetByTimePeriod(DateTimeOffset FromTime, DateTimeOffset ToTime)
        {
            using var connection = _connection.CreateOpenedConnection();
            return connection.Query<HDDMetric>(
               "SELECT * FROM cpumetrics WHERE time BETWEEN @fromTime AND @toTime",
               new
               {
                   fromTime = FromTime.ToUnixTimeSeconds(),
                   toTime = ToTime.ToUnixTimeSeconds()
               }).ToList();
        }
    }
}
