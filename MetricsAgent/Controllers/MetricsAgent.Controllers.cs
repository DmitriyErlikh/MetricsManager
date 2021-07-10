using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.DAL;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using MetricsAgent.Models;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuAgentController : ControllerBase
    {
        private readonly ILogger<CpuAgentController> _logger;
        private ICpuMetricsRepository repository;
        public CpuAgentController(ILogger<CpuAgentController> logger, ICpuMetricsRepository repository)
        {
            this.repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuAgentController");
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsCPU([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Привет! Это наше первое сообщение в лог");
            return Ok();
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            repository.Create(new CpuMetric
            {
                Time = request.Time,
                Value = request.Value
            });

            return Ok();
        }
    }
    [Route("api/metrics/dotnet")]
    public class DotnetAgentController : ControllerBase
    {
        private readonly ILogger<DotnetAgentController> _logger;
        private IDotnetMetricsRepository repository;
        public DotnetAgentController(ILogger<DotnetAgentController> logger, IDotnetMetricsRepository repository)
        {
            this.repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuAgentController");
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsDotnet([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Привет! Это наше первое сообщение в лог");
            return Ok();
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] DotnetMetricCreateRequest request)
        {
            repository.Create(new DotnetMetric
            {
                Time = request.Time,
                Value = request.Value
            });

            return Ok();
        }      
    }
    [Route("api/metrics/hdd")]
    public class HDDAgentController : ControllerBase
    {
        private readonly ILogger<HDDAgentController> _logger;
        private IHDDMetricsRepository repository;
        public HDDAgentController(ILogger<HDDAgentController> logger, IHDDMetricsRepository repository)
        {
            this.repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuAgentController");
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsHDD([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Привет! Это наше первое сообщение в лог");
            return Ok();
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] HDDMetricCreateRequest request)
        {
            repository.Create(new HDDMetric
            {
                Time = request.Time,
                Value = request.Value
            });

            return Ok();
        }
    }
    [Route("api/metrics/hdd")]
    public class NetworkAgentController : ControllerBase
    {
        private readonly ILogger<NetworkAgentController> _logger;
        private INetworkMetricsRepository repository;
        public NetworkAgentController(ILogger<NetworkAgentController> logger, INetworkMetricsRepository repository)
        {
            this.repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuAgentController");
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsNetwork([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Привет! Это наше первое сообщение в лог");
            return Ok();
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        {
            repository.Create(new NetworkMetric
            {
                Time = request.Time,
                Value = request.Value
            });

            return Ok();
        }
    }
    [Route("api/metrics/ram")]
    public class RAMAgentController : ControllerBase
    {
        private readonly ILogger<RAMAgentController> _logger;
        private IRAMMetricsRepository repository;
        public RAMAgentController(ILogger<RAMAgentController> logger, IRAMMetricsRepository repository)
        {
            this.repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuAgentController");
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsRAM([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Привет! Это наше первое сообщение в лог");
            return Ok();
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RAMMetricCreateRequest request)
        {
            repository.Create(new RAMMetric
            {
                Time = request.Time,
                Value = request.Value
            });

            return Ok();
        }
    }
}
