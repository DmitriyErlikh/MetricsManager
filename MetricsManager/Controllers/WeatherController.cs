using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
{
    [Route("api/weather")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly TempHolder _holder;

        public WeatherController(TempHolder holder)
        {
            _holder = holder;
        }
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        [HttpPost("create")]
        public IActionResult Create([FromQuery] int temperatureC, [FromQuery] DateTime date)
        {
            var rng = new Random();
            WeatherForecast weather = new WeatherForecast();
            weather.TemperatureC = temperatureC;
            weather.Date = date;
            weather.Summary = Summaries[rng.Next(Summaries.Length)];
            _holder.weatherHolder.Add(weather);
            return Ok();
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime date, [FromQuery] int newTemperatureC)
        {
            for (int i = 0; i < _holder.weatherHolder.Count; i++)
            {
                if (_holder.weatherHolder[i].Date == date)
                    _holder.weatherHolder[i].TemperatureC = newTemperatureC;
            }

            return Ok("Данные обновлены");
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime dateFrom, [FromQuery] DateTime dateTo)
        {
            for (int i = 0; i < _holder.weatherHolder.Count; i++)
            {
                if (_holder.weatherHolder[i].Date >= dateFrom && _holder.weatherHolder[i].Date <= dateTo)
                {
                    _holder.weatherHolder.RemoveAt(i);
                }
            }
            return Ok("Данные за указанные период удалены");
        }

        [HttpGet("readbetween")]
        public IActionResult ReadBetween([FromQuery] DateTime dateFrom, [FromQuery] DateTime dateTo)
        {
            List<WeatherForecast> arr = new List<WeatherForecast>();
            for (int i = 0; i < _holder.weatherHolder.Count; i++)
            {
                if (_holder.weatherHolder[i].Date >= dateFrom && _holder.weatherHolder[i].Date <= dateTo)
                {
                    arr.Add(_holder.weatherHolder[i]);
                }
            }
            return Ok(arr);
        }
    }
}