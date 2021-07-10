using MetricsAgent.DAL;
using Quartz;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.Models;

namespace MetricsAgent.Jobs
{
    public class NetworkMetricJob : IJob
    {
        private INetworkMetricsRepository _repository;
        private PerformanceCounter _networkCounter;


        public NetworkMetricJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            _networkCounter = new PerformanceCounter("Network Adapter", "Bytes Total/sec", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            var NetworkUsageInPercents = Convert.ToInt32(_networkCounter.NextValue());

            var time = DateTimeOffset.UtcNow;

            _repository.Create(new NetworkMetric { Time = time, Value = NetworkUsageInPercents });

            return Task.CompletedTask;

        }
    }
}
