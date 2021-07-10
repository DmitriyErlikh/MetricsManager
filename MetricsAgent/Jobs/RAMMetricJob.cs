using MetricsAgent.DAL;
using Quartz;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.Models;

namespace MetricsAgent.Jobs
{
    public class RAMMetricJob : IJob
    {
        private IRAMMetricsRepository _repository;
        private PerformanceCounter _RAMCounter;


        public RAMMetricJob(IRAMMetricsRepository repository)
        {
            _repository = repository;
            _RAMCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var RAMUsageInPercents = Convert.ToInt32(_RAMCounter.NextValue());

            var time = DateTimeOffset.UtcNow;

            _repository.Create(new RAMMetric { Time = time, Value = RAMUsageInPercents });

            return Task.CompletedTask;

        }
    }
}
