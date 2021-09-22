using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using DynamicJsonPropertyNamingPolicy.JsonNamingPolicies;

namespace DynamicJsonPropertyNamingPolicy.Extensions
{
    /// <summary>
    /// Extensions for the <cref>Microsoft.AspNetCore.Http.HttpContext</cref> class.
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
        /// Get the <cref>System.Text.Json.JsonSerializerOptions</cref> based on the request headers of the <paramref name="context" />.
        /// </summary>
        /// <param name="context">The <cref>Microsoft.AspNetCore.Http.HttpContext</cref> to inspect.</param>
        /// <returns>The <cref>System.Text.Json.JsonSerializerOptions</cref> that should be used based on the headers.</returns>
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
        /// Get the <cref>System.Text.Json.JsonNamingPolicy</cref> based on the request headers of the <paramref name="context" />.
        /// </summary>
        /// <param name="context">The <cref>Microsoft.AspNetCore.Http.HttpContext</cref> to inspect.</param>
        /// <returns>The <cref>System.Text.Json.JsonNamingPolicy</cref> that should be used based on the headers.</returns>
        public static JsonNamingPolicy? GetJsonNamingPolicy(this HttpContext context)
        {
            return context.GetJsonSerializerOptions().PropertyNamingPolicy;
        }
    }
}
