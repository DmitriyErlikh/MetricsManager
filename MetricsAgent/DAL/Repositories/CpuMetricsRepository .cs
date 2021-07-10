using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MetricsAgent.DAL;
using Dapper;
using System.Linq;
using MetricsAgent.Models;

namespace MetricsAgent.DAL
{
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {
        
    }

    public class CpuMetricsRepository : ICpuMetricsRepository
    {        
        IConnectionManager _connection;
        public CpuMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(CpuMetric item)
        {
            using var connection = _connection.CreateOpenedConnection();

            connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds()
                });
        }
       
        public IList<CpuMetric> GetByTimePeriod(DateTimeOffset FromTime, DateTimeOffset ToTime)
        {
            using var connection = _connection.CreateOpenedConnection();
            return connection.Query<CpuMetric>(
               "SELECT * FROM cpumetrics WHERE time BETWEEN @fromTime AND @toTime",
               new
               {
                   fromTime = FromTime.ToUnixTimeSeconds(),
                   toTime = ToTime.ToUnixTimeSeconds()
               }).ToList();         
        }    
    }
}
