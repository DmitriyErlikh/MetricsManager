using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.DAL;
using System.Data.SQLite;

namespace MetricsAgent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            ConfigureSqlLiteConnection(services);
            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddSingleton<IDotnetMetricsRepository, DotnetMetricsRepository>();
            services.AddSingleton<IHDDMetricsRepository, HDDMetricsRepository>();
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddSingleton<IRAMMetricsRepository, RAMMetricsRepository>();
        }

        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
            const string connectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            PrepareSchema(connection);
        }

        private void PrepareSchema(SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(connection))
            {
                // задаем новый текст команды для выполнения
                // удаляем таблицу с метриками если она существует в базе данных
                command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
                // отправляем запрос в базу данных
                command.ExecuteNonQuery();
                command.CommandText = "DROP TABLE IF EXISTS dotnetmetrics";
                command.ExecuteNonQuery();
                command.CommandText = "DROP TABLE IF EXISTS hddmetrics";
                command.ExecuteNonQuery();
                command.CommandText = "DROP TABLE IF EXISTS networkmetrics";
                command.ExecuteNonQuery();
                command.CommandText = "DROP TABLE IF EXISTS rammetrics";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY,
                    value INT, time INT)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO cpumetrics(value, time) VALUES(50,0)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO cpumetrics(value, time) VALUES(55,1)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO cpumetrics(value, time) VALUES(60,2)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO cpumetrics(value, time) VALUES(65,3)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO cpumetrics(value, time) VALUES(70,4)";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE dotnetmetrics(id INTEGER PRIMARY KEY,
                    value INT, time INT)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO dotnetmetrics(value, time) VALUES(50,0)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO dotnetmetrics(value, time) VALUES(55,1)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO dotnetmetrics(value, time) VALUES(60,2)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO dotnetmetrics(value, time) VALUES(65,3)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO dotnetmetrics(value, time) VALUES(70,4)";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE hddmetrics(id INTEGER PRIMARY KEY,
                    value INT, time INT)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO hddmetrics(value, time) VALUES(50,0)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO hddmetrics(value, time) VALUES(55,1)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO hddmetrics(value, time) VALUES(60,2)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO hddmetrics(value, time) VALUES(65,3)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO hddmetrics(value, time) VALUES(70,4)";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE networkmetrics(id INTEGER PRIMARY KEY,
                    value INT, time INT)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO networkmetrics(value, time) VALUES(50,0)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO networkmetrics(value, time) VALUES(55,1)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO networkmetrics(value, time) VALUES(60,2)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO networkmetrics(value, time) VALUES(65,3)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO networkmetrics(value, time) VALUES(70,4)";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE rammetrics(id INTEGER PRIMARY KEY,
                    value INT, time INT)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO rammetrics(value, time) VALUES(50,0)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO rammetrics(value, time) VALUES(55,1)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO rammetrics(value, time) VALUES(60,2)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO rammetrics(value, time) VALUES(65,3)";
                command.ExecuteNonQuery();
                command.CommandText = @"INSERT INTO rammetrics(value, time) VALUES(70,4)";
                command.ExecuteNonQuery();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
