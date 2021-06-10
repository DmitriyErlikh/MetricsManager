using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RAMMetricsAgentController : ControllerBase
    {
        private readonly ILogger<RAMMetricsAgentController> _logger;

        public RAMMetricsAgentController(ILogger<RAMMetricsAgentController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RamAgentController");
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsRAM([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Привет! Это наше первое сообщение в лог");
            return Ok();
        }
    }
}
