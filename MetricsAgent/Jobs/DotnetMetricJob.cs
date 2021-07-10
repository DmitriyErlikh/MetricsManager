using MetricsAgent.DAL;
using Quartz;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.Models;

namespace MetricsAgent.Jobs
{
    public class DotnetMetricJob : IJob
    {
        private IDotnetMetricsRepository _repository;
        private PerformanceCounter _dotnetCounter;


        public DotnetMetricJob(IDotnetMetricsRepository repository)
        {
            _repository = repository;
            _dotnetCounter = new PerformanceCounter(".NET CLR Memory", "# Total committed Bytes", "_Global_");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var DotnetUsageInPercents = Convert.ToInt32(_dotnetCounter.NextValue());

            var time = DateTimeOffset.UtcNow;

            _repository.Create(new DotnetMetric { Time = time, Value = DotnetUsageInPercents });

            return Task.CompletedTask;

        }
    }
}
