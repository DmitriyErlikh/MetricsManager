using System;
using System.Collections.Generic;

namespace MetricsAgent.Responses
{
    public class AllCpuMetricsResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }
    public class CpuMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }

    public class AllDotnetMetricsResponse
    {
        public List<DotnetMetricDto> Metrics { get; set; }
    }
    public class DotnetMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }

    public class AllHDDMetricsResponse
    {
        public List<HDDMetricDto> Metrics { get; set; }
    }
    public class HDDMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }

    public class AllNetworkMetricsResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; }
    }
    public class NetworkMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }

    public class AllRAMMetricsResponse
    {
        public List<RAMMetricDto> Metrics { get; set; }
    }
    public class RAMMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}