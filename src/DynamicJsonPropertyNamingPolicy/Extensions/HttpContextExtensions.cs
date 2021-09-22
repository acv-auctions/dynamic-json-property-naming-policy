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
        /// <summary>
        /// Get the <cref>System.Text.Json.JsonNamingPolicy</cref> based on the request headers of the <paramref name="context" />.
        /// </summary>
        /// <param name="context">The <cref>Microsoft.AspNetCore.Http.HttpContext</cref> to inspect.</param>
        /// <returns>The <cref>System.Text.Json.JsonNamingPolicy</cref> that should be used based on the headers.</returns>
        public static JsonNamingPolicy? GetJsonNamingPolicy(this HttpContext context)
        {
            // Default to snake for backwards compatibility
            context.Request.Headers.TryGetValue("json-naming-strategy", out StringValues name);
            return name.ToString() switch
            {
                "pascal" => JsonNamingPolicyOptions.PascalCase,
                "camel" => JsonNamingPolicyOptions.CamelCase,
                _ => JsonNamingPolicyOptions.SnakeCase
            };
        }
    }
}
