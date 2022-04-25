using System;
using System.Text.Json;
using DynamicJsonPropertyNamingPolicy.Extensions;

namespace DynamicJsonPropertyNamingPolicy.JsonNamingPolicies
{
    /// <summary>
    /// A <see cref="System.Text.Json.JsonNamingPolicy" /> that converts names to snake_case.
    /// </summary>
    public class SnakeCaseJsonNamingPolicy : JsonNamingPolicy
    {
        internal SnakeCaseJsonNamingPolicy()
        {

        }

        /// <inheritdoc />
        public override string ConvertName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return name.ToSnakeCase();
        }
    }
}
