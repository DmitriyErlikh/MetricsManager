﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MetricsManager.DAL.Support;
using MetricsManager.DAL.Interfaces;
using MetricsManager.Models;

namespace MetricsManager.DAL.Repositories
{
    public class HDDMetricRepository : IHDDMetricRepository
    {
        private IConnectionManager _connection = new IConnectionManager();

        public IList<HDDMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = _connection.CreateOpenedConnection();

            return connection.Query<HDDMetric>(
                "SELECT * FROM cpumetrics WHERE time BETWEEN @fromTime AND @toTime",
                new
                {
                    fromTime = fromTime.ToUnixTimeSeconds(),
                    toTime = toTime.ToUnixTimeSeconds()
                }).ToList();
        }

        public IList<HDDMetric> GetMetricsFromAgent(int id, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = _connection.CreateOpenedConnection();

            return connection.Query<HDDMetric>(
                "SELECT * FROM cpumetrics WHERE AgentId = @agentId AND time BETWEEN @fromTime AND @toTime",
                new
                {
                    agentId = id,
                    fromTime = fromTime.ToUnixTimeSeconds(),
                    toTime = toTime.ToUnixTimeSeconds()
                }).ToList();
        }

        public DateTimeOffset GetLastDateFromAgent(int agentId)
        {
            using var connection = _connection.CreateOpenedConnection();

            return connection.ExecuteScalar<DateTimeOffset>("SELECT MAX(Time) FROM cpumetrics WHERE AgentId = @AgentId",
                new
                {
                    AgentId = agentId
                });
        }

        public void Create(HDDMetric item)
        {
            using var connection = _connection.CreateOpenedConnection();

            connection.Execute("INSERT INTO cpumetrics(agentId ,value, time) VALUES(@agentId, @value, @time)",
                new
                {
                    agentId = item.AgentId,
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds()
                });
        }
    }
}