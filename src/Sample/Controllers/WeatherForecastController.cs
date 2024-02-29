using Microsoft.AspNetCore.Mvc;
using DynamicJsonPropertyNamingPolicy.Extensions;

namespace DynamicJsonPropertyNamingPolicy.Sample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    private readonly ILogger<WeatherForecastController> _logger = logger;

    [HttpGet]
    public ActionResult<IEnumerable<WeatherForecast>> Get([FromHeader(Name = "json-naming-strategy")] string? jsonNamingStrategy = null)
    {
        var namingPolicy = HttpContext.GetJsonNamingPolicy();
        _logger.LogInformation("{Header} received as header, Naming Policy {JsonNamingPolicy}", jsonNamingStrategy, namingPolicy?.GetType()?.Name ?? "JsonDefaultNamingPolicy");

        var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            TimeStamp = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        return new JsonResult(result, HttpContext.GetJsonSerializerOptions());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<string> Post([FromBody] WeatherForecast forecast, [FromHeader(Name = "json-naming-strategy")] string? jsonNamingStrategy = null)
    {
        var namingPolicy = HttpContext.GetJsonNamingPolicy();
        _logger.LogInformation("{Header} received as header, Naming Policy {JsonNamingPolicy}", jsonNamingStrategy, namingPolicy?.GetType()?.Name ?? "JsonDefaultNamingPolicy");
        _logger.LogInformation("Input Model {InputModel}", forecast.ToString());

        if (forecast.TimeStamp.Equals(DateTime.MinValue))
            return BadRequest();

        return forecast.TimeStamp.ToString();
    }
}
