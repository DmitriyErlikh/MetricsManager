using System;

namespace MetricsAgent.Requests
{
    public class CpuMetricCreateRequest
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
    public class DotnetMetricCreateRequest
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
    public class HDDMetricCreateRequest
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
    public class NetworkMetricCreateRequest
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
    public class RAMMetricCreateRequest
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
