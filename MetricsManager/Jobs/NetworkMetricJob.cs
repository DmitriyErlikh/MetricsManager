﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetricsManager.Client;
using MetricsManager.DAL.Interfaces;
using MetricsManager.Models;
using MetricsManager.Requests;
using Microsoft.Extensions.Logging;
using Quartz;
using static MetricsManager.Requests.MetricsManager;

namespace MetricsManager.Jobs
{
    [DisallowConcurrentExecution]
    public class NetworkMetricJob : IJob
    {
        private readonly INetworkMetricRepository _repository;
        private readonly IAgentRepository _agent;
        private readonly ILogger<NetworkMetricJob> _logger;
        private readonly IMetricsAgentClient _client;
        private readonly IMapper _mapper;

        public NetworkMetricJob(
        INetworkMetricRepository repository,
        IAgentRepository agent,
        ILogger<NetworkMetricJob> logger,
        IMetricsAgentClient client,
        IMapper mapper)
        {
            _repository = repository;
            _agent = agent;
            _logger = logger;
            _client = client;
            _mapper = mapper;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var activeAgents = _agent.GetRegisteredList().Where(x => x.IsEnabled);

            foreach (var agent in activeAgents)
            {
                await RetrieveMetricsFromAgent(agent);
            }
        }

        private async Task RetrieveMetricsFromAgent(AgentInfo agent)
        {
            try
            {
                DateTimeOffset lastDate = _repository.GetLastDateFromAgent(agent.AgentId);
                var response = _client.GetAllNetworkMetrics(new NetworkMetricsApiRequest
                {
                    AgentUrl = agent.AgentUrl,
                    FromTime = lastDate,
                    ToTime = DateTimeOffset.Now

                });

                if (response == null) return;

                foreach (var NetworkMetric in response.Metrics.Select(metric => _mapper.Map<NetworkMetric>(metric)))
                {
                    NetworkMetric.AgentId = agent.AgentId;
                    _repository.Create(NetworkMetric);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

    }
}