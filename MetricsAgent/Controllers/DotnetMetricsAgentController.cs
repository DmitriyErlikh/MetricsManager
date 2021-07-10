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
using AutoMapper;
using MetricsAgent.Models;

namespace MetricsAgent.Controllers
{
    [Route("api/dotnetmetrics")]
    [ApiController]
    public class DotnetMetricsAgentController : ControllerBase
    {
        private readonly ILogger<DotnetMetricsAgentController> _logger;
        private readonly IRepository<DotnetMetric> _repository;
        private readonly IMapper _mapper;

        public DotnetMetricsAgentController(ILogger<DotnetMetricsAgentController> logger, IRepository<DotnetMetric> repository, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsDotnet([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Input: fromTime - {fromTime}; toTime - {toTime}");

            IList<DotnetMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);
            AllDotnetMetricsResponse response = new AllDotnetMetricsResponse()
            {
                Metrics = new List<DotnetMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotnetMetricDto>(metric));
            }
            return Ok(response);
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] DotnetMetric metric)
        {
            _repository.Create(metric);
            return Ok();
        }
    }
}
