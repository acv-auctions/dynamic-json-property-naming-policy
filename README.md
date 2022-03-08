# DynamicJsonPropertyNamingPolicy

[![Nuget](https://img.shields.io/nuget/v/DynamicJsonPropertyNamingPolicy)](https://www.nuget.org/packages/DynamicJsonPropertyNamingPolicy/)

A `System.Text.Json.JsonNamingPolicy` that reacts to request headers.

## Usage

Based on the value of the `json-naming-strategy` request header, this package will determine what type of serialization method to follow when one of the following is used:

1. `camel` for `camelCase`
1. `pascal` for `PascalCase`
1. `snake` for `snake_case`

If nothing is sent in the header, it will default to `snake_case` for compatibility with the systems it was originally built for.

### As an InputFormatter

This method will allow your ASP.NET controllers to correctly deserialize incoming JSON following the `json-naming-strategy`.

```cs
builder.Services.AddControllers(o =>
{
    o.InputFormatters.Insert(0, new DynamicSystemTextJsonInputFormatter());
});
```

Which would then allow you to receive a request body of:

```json
{
    "time_stamp": "2021-09-16T22:05:29.846Z"
}
```

and correctly deserialize it into:

```cs
public record WeatherForecast
{
    public DateTime TimeStamp { get; set; }
}
```

### In JsonSerializerOptions

If you just want to use `JsonSerializerOptions` that apply the correct property naming use the following:

```cs
return new JsonResult(result, HttpContext.GetJsonSerializerOptions());
```

Otherwise, you can just reference the `JsonNamingPolicy` to add additional options:

```cs
var namingPolicy = HttpContext.GetJsonNamingPolicy();
return new JsonResult(result, new JsonSerializerOptions
{
    PropertyNamingPolicy = namingPolicy,
    NumberHandling = JsonNumberHandling.AllowReadingFromString
});
```

This allows you to send a response back to the requestor that will respect their request for a specific serialization strategy.

## Example

You can see usage as an `InputFormatter` in [Program.cs](src/Sample/Program.cs) and as `JsonSerializerOptions` in [WeatherForecastController.cs](src/Sample/Controllers/WeatherForecastController.cs).

Additionally, you can run [the Sample project](src/Sample) with:

```sh
dotnet run
```

And then load https://localhost:5001/swagger to try out some requests. Within the SwaggerUI, you can specify the `json-naming-strategy` header to see the differences.

## Contributing

Please see [CONTRIBUTING.md](CONTRIBUTING.md) for details on how to contribute to the project.
