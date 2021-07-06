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
    public class RAMMetricsAgentController : ControllerBase
    {
        private readonly ILogger<RAMMetricsAgentController> _logger;
        private readonly IRepository<RAMMetric> _repository;

        public RAMMetricsAgentController(ILogger<RAMMetricsAgentController> logger, IRepository<RAMMetric> repository)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsDotnet([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Input: fromTime - {fromTime}; toTime - {toTime}");
            IList<RAMMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);
            AllRAMMetricsResponse response = new AllRAMMetricsResponse();

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new RAMMetricDto
                {
                    Time = metric.Time,
                    Value = metric.Value
                });
            }
            return Ok(response);
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] RAMMetric metric)
        {
            _repository.Create(metric);
            return Ok();
        }
    }
}
