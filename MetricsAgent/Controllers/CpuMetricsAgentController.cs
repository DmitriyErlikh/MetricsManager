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
    [Route("api/cpumetrics")]
    [ApiController]
    public class CpuMetricsAgentController : ControllerBase
    {
        private readonly ILogger<CpuMetricsAgentController> _logger;
        private readonly IRepository<CpuMetric> _repository;
        private readonly IMapper _mapper;

        public CpuMetricsAgentController(ILogger<CpuMetricsAgentController> logger, IRepository<CpuMetric> repository, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsCpu([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Input: fromTime - {fromTime}; toTime - {toTime}");

            IList<CpuMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);
            AllCpuMetricsResponse response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
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
