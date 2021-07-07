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

namespace MetricsAgent.Controllers
{
    [Route("api/hddmetrics")]
    [ApiController]
    public class HDDMetricsAgentController : ControllerBase
    {
        private readonly ILogger<HDDMetricsAgentController> _logger;
        private readonly IRepository<HDDMetric> _repository;
        private readonly IMapper _mapper;

        public HDDMetricsAgentController(ILogger<HDDMetricsAgentController> logger, IRepository<HDDMetric> repository, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsHDD([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Input: fromTime - {fromTime}; toTime - {toTime}");

            IList<HDDMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);
            AllHDDMetricsResponse response = new AllHDDMetricsResponse()
            {
                Metrics = new List<HDDMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HDDMetricDto>(metric));
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
