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
    [Route("api/rammetrics")]
    [ApiController]
    public class RAMMetricsAgentController : ControllerBase
    {
        private readonly ILogger<RAMMetricsAgentController> _logger;
        private readonly IRepository<RAMMetric> _repository;
        private readonly IMapper _mapper;

        public RAMMetricsAgentController(ILogger<RAMMetricsAgentController> logger, IRepository<RAMMetric> repository, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsRAM([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Input: fromTime - {fromTime}; toTime - {toTime}");

            IList<RAMMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);
            AllRAMMetricsResponse response = new AllRAMMetricsResponse()
            {
                Metrics = new List<RAMMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RAMMetricDto>(metric));
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
