using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.DAL
{// маркировочный интерфейс
    // необходим, чтобы проверить работу репозитория на тесте-заглушке
    public interface IRAMMetricsRepository : IRepository<RAMMetric>
    {

    }

    public class RAMMetricsRepository : IRAMMetricsRepository
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        // инжектируем соединение с базой данных в наш репозиторий через конструктор

        public void Create(RAMMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            // создаем команду
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

        public IList<RAMMetric> GetByTimePeriod(DateTimeOffset FromTime, DateTimeOffset ToTime)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);

            cmd.CommandText = "SELECT * FROM cpumetrics WHERE time >= @FromTime AND time<= @ToTime";
            cmd.Parameters.AddWithValue("@FromTime", FromTime.ToUnixTimeSeconds());
            cmd.Parameters.AddWithValue("@ToTime", ToTime.ToUnixTimeSeconds());

            var returnList = new List<RAMMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                while (reader.Read())
                {
                    // добавляем объект в список возврата
                    returnList.Add(new RAMMetric
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
