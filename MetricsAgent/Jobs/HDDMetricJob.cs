using MetricsAgent.DAL;
using Quartz;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.Models;

namespace MetricsAgent.Jobs
{
    public class HDDMetricJob : IJob
    {
        private IHDDMetricsRepository _repository;
        private PerformanceCounter _HDDCounter;


        public HDDMetricJob(IHDDMetricsRepository repository)
        {
            _repository = repository;
            _HDDCounter = new PerformanceCounter("LogicalDisk", "% Free Space", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var HDDUsageInPercents = Convert.ToInt32(_HDDCounter.NextValue());

            var time = DateTimeOffset.UtcNow;

            _repository.Create(new HDDMetric { Time = time, Value = HDDUsageInPercents });

            return Task.CompletedTask;

        }
    }
}
