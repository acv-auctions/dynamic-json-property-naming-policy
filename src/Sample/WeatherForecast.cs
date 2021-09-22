namespace DynamicJsonPropertyNamingPolicy.Sample;

public record WeatherForecast
{
    public DateTime TimeStamp { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}
