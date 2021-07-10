using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.Requests;

namespace MetricsManager.Responses
{
    public class AllCpuMetricResponses
    {
        public class CpuMetricDto
        {
            public int Value { get; set; }
            public int AgentId { get; set; }
            public DateTimeOffset Time { get; set; }
        }
        public List<CpuMetricDto> Metrics { get; set; }

        public AllCpuMetricResponses()
        {
            Metrics = new List<CpuMetricDto>();
        }
    }
    public class AllDotnetMetricResponses
    {
        public class DotnetMetricDto
        {
            public int Value { get; set; }
            public int AgentId { get; set; }
            public DateTimeOffset Time { get; set; }
        }
        public List<DotnetMetricDto> Metrics { get; set; }

        public AllDotnetMetricResponses()
        {
            Metrics = new List<DotnetMetricDto>();
        }
    }
    public class AllHDDMetricResponses
    {
        public class HDDMetricDto
        {
            public int Value { get; set; }
            public int AgentId { get; set; }
            public DateTimeOffset Time { get; set; }
        }
        public List<HDDMetricDto> Metrics { get; set; }

        public AllHDDMetricResponses()
        {
            Metrics = new List<HDDMetricDto>();
        }
    }
    public class AllNetworkMetricResponses
    {
        public class NetworkMetricDto
        {
            public int Value { get; set; }
            public int AgentId { get; set; }
            public DateTimeOffset Time { get; set; }
        }
        public List<NetworkMetricDto> Metrics { get; set; }

        public AllNetworkMetricResponses()
        {
            Metrics = new List<NetworkMetricDto>();
        }
    }
    public class AllRAMMetricResponses
    {
        public class RAMMetricDto
        {
            public int Value { get; set; }
            public int AgentId { get; set; }
            public DateTimeOffset Time { get; set; }
        }
        public List<RAMMetricDto> Metrics { get; set; }

        public AllRAMMetricResponses()
        {
            Metrics = new List<RAMMetricDto>();
        }
    }
    public class JobSchedule
    {
        public JobSchedule(Type jobType, string cronExpression)
        {
            JobType = jobType;
            CronExpression = cronExpression;
        }

        public Type JobType { get; }
        public string CronExpression { get; }

    }
}
