using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsAgentController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsAgentController> _logger;

        public NetworkMetricsAgentController(ILogger<NetworkMetricsAgentController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в NetworkAgentController");
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsNetwork([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Привет! Это наше первое сообщение в лог");
            return Ok();
        }
    }
}
