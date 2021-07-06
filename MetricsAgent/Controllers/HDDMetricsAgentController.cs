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
    public class HDDMetricsAgentController : ControllerBase
    {
        private readonly ILogger<HDDMetricsAgentController> _logger;
        private readonly IRepository<HDDMetric> _repository;

        public HDDMetricsAgentController(ILogger<HDDMetricsAgentController> logger, IRepository<HDDMetric> repository)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsDotnet([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Input: fromTime - {fromTime}; toTime - {toTime}");
            IList<HDDMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);
            AllHDDMetricsResponse response = new AllHDDMetricsResponse();

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new HDDMetricDto
                {
                    Time = metric.Time,
                    Value = metric.Value
                });
            }
            return Ok(response);
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] HDDMetric metric)
        {
            _repository.Create(metric);
            return Ok();
        }
    }
}
