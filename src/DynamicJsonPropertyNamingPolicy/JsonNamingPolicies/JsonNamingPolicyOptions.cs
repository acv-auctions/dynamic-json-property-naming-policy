using System.Text.Json;

namespace DynamicJsonPropertyNamingPolicy.JsonNamingPolicies
{
    /// <summary>
    /// A collection of options that can be used when a <see cref="System.Text.Json.JsonNamingPolicy" /> is needed.
    /// </summary>
    public static class JsonNamingPolicyOptions
    {
        //TODO: Replace these custom ones with Microsoft ones when available

        /// <summary>
        /// PascalCase is the ASP.NET default and internally uses `null` to represent it.
        /// </summary>
        public static readonly JsonNamingPolicy? PascalCase = null;

        /// <summary>
        /// Create a single <see cref="DynamicJsonPropertyNamingPolicy.JsonNamingPolicies.SnakeCaseJsonNamingPolicy" /> that can be reused as needed.
        /// </summary>
        public static readonly JsonNamingPolicy SnakeCase = new SnakeCaseJsonNamingPolicy();

        /// <summary>
        /// ASP.NET provides an internal <see cref="System.Text.Json.JsonCamelCaseNamingPolicy" /> that can be used.
        /// </summary>
        public static readonly JsonNamingPolicy CamelCase = JsonNamingPolicy.CamelCase;
    }
}
