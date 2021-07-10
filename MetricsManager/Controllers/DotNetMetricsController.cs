using System;
using System.Collections.Generic;
using AutoMapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.Models;
using MetricsManager.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static MetricsManager.Responses.AllDotnetMetricResponses;

namespace MetricsManager.Controllers
{
    [ApiController]
    [Route("api/metrics/dotnet")]
    public class DotnetMetricsController : Controller
    {
        private readonly ILogger<DotnetMetricsController> _logger;
        private readonly IDotnetMetricRepository _repository;
        private readonly IMapper _mapper;

        public DotnetMetricsController(
            ILogger<DotnetMetricsController> logger,
            IDotnetMetricRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в DotNetMetricsController");
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent(
            [FromRoute] int agentId,
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Input: agentId: {agentId}; fromTime - {fromTime}; toTime - {toTime}");

            IList<DotnetMetric> metrics = _repository.GetMetricsFromAgent(agentId, fromTime, toTime);
            AllDotnetMetricResponses response = new AllDotnetMetricResponses()
            {
                Metrics = new List<DotnetMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotnetMetricDto>(metric));
            }

            return Ok(response);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromCluster([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Input: fromTime - {fromTime}; toTime - {toTime}");

            IList<DotnetMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);
            AllDotnetMetricResponses response = new AllDotnetMetricResponses()
            {
                Metrics = new List<DotnetMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotnetMetricDto>(metric));
            }

            return Ok(response);
        }
    }
}