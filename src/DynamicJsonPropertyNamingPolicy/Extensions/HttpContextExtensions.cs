using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using DynamicJsonPropertyNamingPolicy.JsonNamingPolicies;

namespace DynamicJsonPropertyNamingPolicy.Extensions
{
    /// <summary>
    /// Extensions for the <see cref="Microsoft.AspNetCore.Http.HttpContext" /> class.
    /// </summary>
    public static class HttpContextExtensions
    {
        internal static readonly string _headerName = "json-naming-strategy";

        // Based on: https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-configure-options
        // it is best to reuse instances of the JsonSerializerOptions.
        internal static readonly JsonSerializerOptions _snakeCaseOptions =
            new() { PropertyNamingPolicy = JsonNamingPolicyOptions.SnakeCase };
        internal static readonly JsonSerializerOptions _camelCaseOptions =
            new() { PropertyNamingPolicy = JsonNamingPolicyOptions.CamelCase };
        internal static readonly JsonSerializerOptions _pascalCaseOptions =
            new() { PropertyNamingPolicy = JsonNamingPolicyOptions.PascalCase };

        /// <summary>
        /// Get the <see cref="System.Text.Json.JsonSerializerOptions" /> based on the request headers of the <paramref name="context" />.
        /// </summary>
        /// <param name="context">The <see cref="Microsoft.AspNetCore.Http.HttpContext" /> to inspect.</param>
        /// <returns>The <see cref="System.Text.Json.JsonSerializerOptions" /> that should be used based on the headers.</returns>
        public static JsonSerializerOptions GetJsonSerializerOptions(this HttpContext context)
        {
            // Default to snake for backwards compatibility
            context.Request.Headers.TryGetValue(_headerName, out StringValues name);
            return name.ToString() switch
            {
                "pascal" => _pascalCaseOptions,
                "camel" => _camelCaseOptions,
                _ => _snakeCaseOptions
            };
        }

        /// <summary>
        /// Get the <see cref="System.Text.Json.JsonNamingPolicy" /> based on the request headers of the <paramref name="context" />.
        /// </summary>
        /// <param name="context">The <see cref="Microsoft.AspNetCore.Http.HttpContext" /> to inspect.</param>
        /// <returns>The <see cref="System.Text.Json.JsonNamingPolicy" /> that should be used based on the headers.</returns>
        public static JsonNamingPolicy? GetJsonNamingPolicy(this HttpContext context)
        {
            return context.GetJsonSerializerOptions().PropertyNamingPolicy;
        }
    }
}
