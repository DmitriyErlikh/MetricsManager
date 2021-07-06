using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data.SQLite;
using MetricsAgent.DAL;
using MetricsAgent.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/cpumetrics")]
    [ApiController]
    public class NetworkMetricsAgentController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsAgentController> _logger;
        private readonly IRepository<NetworkMetric> _repository;

        public NetworkMetricsAgentController(ILogger<NetworkMetricsAgentController> logger, IRepository<NetworkMetric> repository)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsDotnet([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Input: fromTime - {fromTime}; toTime - {toTime}");
            IList<NetworkMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);
            AllNetworkMetricsResponse response = new AllNetworkMetricsResponse();

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new NetworkMetricDto
                {
                    Time = metric.Time,
                    Value = metric.Value
                });
            }
            return Ok(response);
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetric metric)
        {
            _repository.Create(metric);
            return Ok();
        }
    }
}
