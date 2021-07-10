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
using static MetricsManager.Responses.AllRAMMetricResponses;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class RAMMetricsController : ControllerBase
    {
        private readonly ILogger<RAMMetricsController> _logger;
        private readonly IRAMMetricRepository _repository;
        private readonly IMapper _mapper;

        public RAMMetricsController(
            ILogger<RAMMetricsController> logger,
            IRAMMetricRepository repository,
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

            IList<RAMMetric> metrics = _repository.GetMetricsFromAgent(agentId, fromTime, toTime);
            AllRAMMetricResponses response = new AllRAMMetricResponses()
            {
                Metrics = new List<RAMMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RAMMetricDto>(metric));
            }

            return Ok(response);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromCluster([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Input: fromTime - {fromTime}; toTime - {toTime}");

            IList<RAMMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);
            AllRAMMetricResponses response = new AllRAMMetricResponses()
            {
                Metrics = new List<RAMMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RAMMetricDto>(metric));
            }

            return Ok(response);
        }
    }
}