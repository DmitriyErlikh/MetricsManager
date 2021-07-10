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
    public class DotnetMetricJob : IJob
    {
        private readonly IDotnetMetricRepository _repository;
        private readonly IAgentRepository _agent;
        private readonly ILogger<CpuMetricJob> _logger;
        private readonly IMetricsAgentClient _client;
        private readonly IMapper _mapper;

        public DotnetMetricJob(
        IDotnetMetricRepository repository,
        IAgentRepository agent,
        ILogger<CpuMetricJob> logger,
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
                var response = _client.GetAllDotnetMetrics(new DotnetMetricsApiRequest
                {
                    AgentUrl = agent.AgentUrl,
                    FromTime = lastDate,
                    ToTime = DateTimeOffset.Now

                });

                if (response == null) return;

                foreach (var DotnetMetric in response.Metrics.Select(metric => _mapper.Map<DotnetMetric>(metric)))
                {
                    DotnetMetric.AgentId = agent.AgentId;
                    _repository.Create(DotnetMetric);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

    }
}