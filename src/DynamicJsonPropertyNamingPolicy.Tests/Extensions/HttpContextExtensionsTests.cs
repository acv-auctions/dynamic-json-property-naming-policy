using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using DynamicJsonPropertyNamingPolicy.Extensions;
using DynamicJsonPropertyNamingPolicy.JsonNamingPolicies;
using Xunit;

namespace DynamicJsonPropertyNamingPolicy.Tests.Extensions
{
    public class HttpContextExtensionsTests
    {
        public static IEnumerable<object[]> NamingPolicyData
            => new[]
            {
                new object[] {"snake", JsonNamingPolicyOptions.SnakeCase},
                new object[] {"camel", JsonNamingPolicyOptions.CamelCase},
                new object[] {"pascal", JsonNamingPolicyOptions.PascalCase},
                new object[] {"unclegeorge", JsonNamingPolicyOptions.SnakeCase},
                new object[] {null, JsonNamingPolicyOptions.SnakeCase}
            };

        [Theory]
        [MemberData(nameof(NamingPolicyData))]
        public void GetJsonNamingPolicyTest(string headerValue, JsonNamingPolicy expected)
        {
            // Arrange
            HttpContext context = new DefaultHttpContext();
            if (headerValue != null)
            {
                context.Request.Headers.Add("json-naming-strategy", headerValue);
            }

            // Act
            JsonNamingPolicy actual = context.GetJsonNamingPolicy();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
