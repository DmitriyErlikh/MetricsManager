using System;
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
    public class RAMMetricJob : IJob
    {
        private readonly IRAMMetricRepository _repository;
        private readonly IAgentRepository _agent;
        private readonly ILogger<RAMMetricJob> _logger;
        private readonly IMetricsAgentClient _client;
        private readonly IMapper _mapper;

        public RAMMetricJob(
        IRAMMetricRepository repository,
        IAgentRepository agent,
        ILogger<RAMMetricJob> logger,
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
                var response = _client.GetAllRAMMetrics(new RAMMetricsApiRequest
                {
                    AgentUrl = agent.AgentUrl,
                    FromTime = lastDate,
                    ToTime = DateTimeOffset.Now

                });

                if (response == null) return;

                foreach (var RAMMetric in response.Metrics.Select(metric => _mapper.Map<RAMMetric>(metric)))
                {
                    RAMMetric.AgentId = agent.AgentId;
                    _repository.Create(RAMMetric);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

    }
}