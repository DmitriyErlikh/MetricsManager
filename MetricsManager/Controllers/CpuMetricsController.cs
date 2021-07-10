using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.Models;
using MetricsManager.Responses;
using Microsoft.Extensions.Logging;
using static MetricsManager.Responses.AllCpuMetricResponses;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly ICpuMetricRepository _repository;
        private readonly IMapper _mapper;

        public CpuMetricsController(
            ILogger<CpuMetricsController> logger,
            ICpuMetricRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }
        /// <summary>
        /// Получает метрики CPU на заданном диапазоне времени от агента
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET agent/1/from/2021-07-20/to/2021-07-22
        ///
        /// </remarks>
        /// <param name="agentId">ID агента</param>
        /// <param name="fromTime">начальная метка времени</param>
        /// <param name="toTime">конечная метка времени</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени</returns>
        /// <response code="201">Если все хорошо</response>
        /// <response code="400">если передали не правильные параметры</response>
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent(
            [FromRoute] int agentId,
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Input: agentId: {agentId}; fromTime - {fromTime}; toTime - {toTime}");

            IList<CpuMetric> metrics = _repository.GetMetricsFromAgent(agentId, fromTime, toTime);
            AllCpuMetricResponses response = new AllCpuMetricResponses()
            {
                Metrics = new List<CpuMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }

            return Ok(response);
        }
        /// <summary>
        /// Получает метрики CPU на заданном диапазоне времени от агента в заданном диапазоне времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET cluster/from/2021-07-20/to/2021-07-22
        ///
        /// </remarks>
        /// <param name="fromTime">начальная метрка времени</param>
        /// <param name="toTime">конечная метрка времени</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени</returns>
        /// <response code="201">Если все хорошо</response>
        /// <response code="400">если передали не правильные параметры</response> 
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromCluster([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Input: fromTime - {fromTime}; toTime - {toTime}");

            IList<CpuMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);
            AllCpuMetricResponses response = new AllCpuMetricResponses()
            {
                Metrics = new List<CpuMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }

            return Ok(response);
        }
    }
}