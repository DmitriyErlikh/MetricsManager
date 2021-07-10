using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace MetricsManager.DAL.Support
{
    public class IConnectionManager
    {
        public const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        public SQLiteConnection CreateOpenedConnection()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection.OpenAndReturn();
        }
    }
}
