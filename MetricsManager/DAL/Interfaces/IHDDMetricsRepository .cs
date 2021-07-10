using System;
using System.Collections.Generic;
using MetricsManager.DAL.Repositories;
using MetricsManager.Models;

namespace MetricsManager.DAL.Interfaces
{
    public interface IHDDMetricRepository : IRepository<HDDMetric>
    {
        IList<HDDMetric> GetMetricsFromAgent(int id, DateTimeOffset fromTime, DateTimeOffset toTime);
        DateTimeOffset GetLastDateFromAgent(int agentId);
    }
}
