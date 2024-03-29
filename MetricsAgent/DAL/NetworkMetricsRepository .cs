﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.DAL
{// маркировочный интерфейс
    // необходим, чтобы проверить работу репозитория на тесте-заглушке
    public interface INetworkMetricsRepository : IRepository<NetworkMetric>
    {

    }

    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        IConnectionManager _connection;

        public void Create(NetworkMetric item)
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

        public IList<NetworkMetric> GetByTimePeriod(DateTimeOffset FromTime, DateTimeOffset ToTime)
        {
            using var connection = _connection.CreateOpenedConnection();
            using var cmd = new SQLiteCommand(connection);

            cmd.CommandText = "SELECT * FROM cpumetrics WHERE time >= @FromTime AND time<= @ToTime";
            cmd.Parameters.AddWithValue("@FromTime", FromTime.ToUnixTimeSeconds());
            cmd.Parameters.AddWithValue("@ToTime", ToTime.ToUnixTimeSeconds());

            var returnList = new List<NetworkMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                while (reader.Read())
                {
                    // добавляем объект в список возврата
                    returnList.Add(new NetworkMetric
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        // налету преобразуем прочитанные секунды в метку времени
                        Time = DateTimeOffset.FromFileTime(reader.GetInt32(2))
                    });
                }
            }

            return returnList;
        }
    }
}
