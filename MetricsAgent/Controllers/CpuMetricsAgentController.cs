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
    public class CpuMetricsAgentController : ControllerBase
    {
        private readonly ILogger<CpuMetricsAgentController> _logger;
        private readonly IRepository<CpuMetric> _repository;

        public CpuMetricsAgentController(ILogger<CpuMetricsAgentController> logger, IRepository<CpuMetric> repository)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsDotnet([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Input: fromTime - {fromTime}; toTime - {toTime}");
            IList<CpuMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);
            AllCpuMetricsResponse response = new AllCpuMetricsResponse();

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new CpuMetricDto
                {
                    Time = metric.Time,
                    Value = metric.Value
                });
            }
            return Ok(response);
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetric metric)
        {
            _repository.Create(metric);
            return Ok();
        }
    }
}
