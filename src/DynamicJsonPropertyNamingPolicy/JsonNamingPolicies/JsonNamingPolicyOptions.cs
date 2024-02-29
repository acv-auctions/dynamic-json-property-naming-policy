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
        /// PascalCase is the .NET default and internally uses `null` to represent it.
        /// </summary>
        public static readonly JsonNamingPolicy? PascalCase = null;

        /// <summary>
        /// .NET provides an internal <see cref="System.Text.Json.JsonNamingPolicy" /> that can be used.
        /// </summary>
        public static readonly JsonNamingPolicy SnakeCase = JsonNamingPolicy.SnakeCaseLower;

        /// <summary>
        /// .NET provides an internal <see cref="System.Text.Json.JsonNamingPolicy" /> that can be used.
        /// </summary>
        public static readonly JsonNamingPolicy CamelCase = JsonNamingPolicy.CamelCase;
    }
}
