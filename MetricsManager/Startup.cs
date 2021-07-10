using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentMigrator.Runner;
using MetricsManager.Client;
using MetricsManager.DAL.Support;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Repositories;
using MetricsManager.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using MetricsManager.Responses;
using MetricsAgent.DAL;
using Polly;

namespace MetricsManager
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
            services.AddSingleton<IAgentRepository, AgentRepository>();
            services.AddSingleton<ICpuMetricRepository, CpuMetricRepository>();
            services.AddSingleton<IDotnetMetricRepository, DotnetMetricRepository>();
            services.AddSingleton<IHDDMetricRepository, HDDMetricRepository>();
            services.AddSingleton<INetworkMetricRepository, NetworkMetricRepository>();
            services.AddSingleton<IRAMMetricRepository, RAMMetricRepository>();

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<CpuMetricJob>();
            services.AddSingleton<DotnetMetricJob>();
            services.AddSingleton<HDDMetricJob>();
            services.AddSingleton<NetworkMetricJob>();
            services.AddSingleton<RAMMetricJob>();
            services.AddSingleton(new JobSchedule(
                typeof(CpuMetricJob),
                "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(
                typeof(DotnetMetricJob),
                "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(
                typeof(HDDMetricJob),
                "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(
                typeof(NetworkMetricJob),
                "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(
                typeof(RAMMetricJob),
                "0/5 * * * * ?"));

            services.AddHostedService<QuartzHostedService>();

            MapperConfiguration mapperConfiguration = new MapperConfiguration(
                mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();

            services.AddSingleton(mapper);
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb 
                    .AddSQLite()
                    .WithGlobalConnectionString(DAL.Support.IConnectionManager.ConnectionString)
                    .ScanIn(typeof(Startup).Assembly).For.Migrations()
                ).AddLogging(lb => lb
                    .AddFluentMigratorConsole());

            services.AddHttpClient<IMetricsAgentClient, MetricsAgentClient>()
                .AddTransientHttpErrorPolicy(p
                    => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricsManager", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MetricsManager v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            migrationRunner.MigrateUp();
        }
    }
}