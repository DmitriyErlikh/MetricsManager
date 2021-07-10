using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Requests
{
    public class MetricsManager
    {
        public class CpuMetricsApiRequest
        {
            public Uri AgentUrl { get; set; }
            public DateTimeOffset FromTime { get; set; }
            public DateTimeOffset ToTime { get; set; }
        }
        public class DotnetMetricsApiRequest
        {
            public Uri AgentUrl { get; set; }
            public DateTimeOffset FromTime { get; set; }
            public DateTimeOffset ToTime { get; set; }
        }
        public class HDDMetricsApiRequest
        {
            public Uri AgentUrl { get; set; }
            public DateTimeOffset FromTime { get; set; }
            public DateTimeOffset ToTime { get; set; }
        }
        public class NetworkMetricsApiRequest
        {
            public Uri AgentUrl { get; set; }
            public DateTimeOffset FromTime { get; set; }
            public DateTimeOffset ToTime { get; set; }
        }
        public class RAMMetricsApiRequest
        {
            public Uri AgentUrl { get; set; }
            public DateTimeOffset FromTime { get; set; }
            public DateTimeOffset ToTime { get; set; }
        }
    }
}
