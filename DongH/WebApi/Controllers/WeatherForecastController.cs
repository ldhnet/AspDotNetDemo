using DongH.Tool.Helper;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("111111111111");
            _logger.LogDebug("3333333333333333");
            _logger.LogWarning("444444444444444444");
            _logger.LogTrace("5555555555555555555");


            LogHelper.Info(nameof(WeatherForecastController) + "=¡·LogHelper111111111111");
            LogHelper.Error(nameof(WeatherForecastController) + "=¡·LogHelper2222222222222");
            LogHelper.Debug(nameof(WeatherForecastController) + "=¡·LogHelper3333333333333333");
            LogHelper.Warn(nameof(WeatherForecastController) + "=¡·LogHelper444444444444444444");
            LogHelper.Trace(nameof(WeatherForecastController) + "=¡·LogHelper5555555555555555555");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}