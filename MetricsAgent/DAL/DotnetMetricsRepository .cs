using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.DAL
{
    public interface IDotnetMetricsRepository : IRepository<DotnetMetric>
    {

    }

    public class DotnetMetricsRepository : IDotnetMetricsRepository
    {
        IConnectionManager _connection;

        public void Create(DotnetMetric item)
        {
            using var connection = _connection.CreateOpenedConnection();

            using var cmd = new SQLiteCommand(connection);
            // прописываем в команду SQL запрос на вставку данных
            cmd.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(@value, @time)";

            // добавляем параметры в запрос из нашего объекта
            cmd.Parameters.AddWithValue("@value", item.Value);

            // в таблице будем хранить время в секундах, потому преобразуем перед записью в секунды
            // через свойство
            cmd.Parameters.AddWithValue("@time", item.Time.Second);
            // подготовка команды к выполнению
            cmd.Prepare();

            // выполнение команды
            cmd.ExecuteNonQuery();
        }

        public IList<DotnetMetric> GetByTimePeriod(DateTimeOffset FromTime, DateTimeOffset ToTime)
        {
            using var connection = _connection.CreateOpenedConnection();

            using var cmd = new SQLiteCommand(connection);

            cmd.CommandText = "SELECT * FROM cpumetrics WHERE time >= @FromTime AND time<= @ToTime";
            cmd.Parameters.AddWithValue("@FromTime", FromTime.ToUnixTimeSeconds());
            cmd.Parameters.AddWithValue("@ToTime", ToTime.ToUnixTimeSeconds());

            var returnList = new List<DotnetMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new DotnetMetric
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),                        
                        Time = DateTimeOffset.FromFileTime(reader.GetInt32(2))
                    });
                }
            }

            return returnList;
        }
    }
}
