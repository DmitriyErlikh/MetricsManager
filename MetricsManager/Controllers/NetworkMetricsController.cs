using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.Models;
using MetricsManager.Responses;
using Microsoft.Extensions.Logging;
using static MetricsManager.Responses.AllNetworkMetricResponses;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly INetworkMetricRepository _repository;
        private readonly IMapper _mapper;

        public NetworkMetricsController(
            ILogger<NetworkMetricsController> logger,
            INetworkMetricRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent(
            [FromRoute] int agentId,
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Input: agentId: {agentId}; fromTime - {fromTime}; toTime - {toTime}");

            IList<NetworkMetric> metrics = _repository.GetMetricsFromAgent(agentId, fromTime, toTime);
            AllNetworkMetricResponses response = new AllNetworkMetricResponses()
            {
                Metrics = new List<NetworkMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
            }

            return Ok(response);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromCluster([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Input: fromTime - {fromTime}; toTime - {toTime}");

            IList<NetworkMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);
            AllNetworkMetricResponses response = new AllNetworkMetricResponses()
            {
                Metrics = new List<NetworkMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
            }

            return Ok(response);
        }
    }
}