using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MetricsAgent.DAL;
using Dapper;
using System.Linq;
using MetricsAgent.Models;

namespace MetricsAgent.DAL
{
    public interface IRAMMetricsRepository : IRepository<RAMMetric>
    {

    }

    public class RAMMetricsRepository : IRAMMetricsRepository
    {
        IConnectionManager _connection;
        public RAMMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(RAMMetric item)
        {
            using var connection = _connection.CreateOpenedConnection();

            connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds()
                });
        }

        public IList<RAMMetric> GetByTimePeriod(DateTimeOffset FromTime, DateTimeOffset ToTime)
        {
            using var connection = _connection.CreateOpenedConnection();
            return connection.Query<RAMMetric>(
               "SELECT * FROM cpumetrics WHERE time BETWEEN @fromTime AND @toTime",
               new
               {
                   fromTime = FromTime.ToUnixTimeSeconds(),
                   toTime = ToTime.ToUnixTimeSeconds()
               }).ToList();
        }
    }
}
