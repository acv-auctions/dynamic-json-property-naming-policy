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
        public static IEnumerable<object[]> NamingOptionData
            => new[]
            {
                new object[] {"snake", HttpContextExtensions._snakeCaseOptions},
                new object[] {"camel", HttpContextExtensions._camelCaseOptions},
                new object[] {"pascal", HttpContextExtensions._pascalCaseOptions},
                new object[] {"unclegeorge", HttpContextExtensions._snakeCaseOptions},
                new object[] {null, HttpContextExtensions._snakeCaseOptions}
            };

        [Theory]
        [MemberData(nameof(NamingOptionData))]
        public void GetJsonNamingPolicyTest(string headerValue, JsonSerializerOptions expected)
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
            Assert.Equal(expected.PropertyNamingPolicy, actual);
        }

        [Theory]
        [MemberData(nameof(NamingOptionData))]
        public void GetJsonSerializerOptionsTest(string headerValue, JsonSerializerOptions expected)
        {
            // Arrange
            HttpContext context = new DefaultHttpContext();
            if (headerValue != null)
            {
                context.Request.Headers.Add("json-naming-strategy", headerValue);
            }

            // Act
            JsonSerializerOptions actual = context.GetJsonSerializerOptions();

            // Assert
            Assert.Same(expected, actual);
        }
    }
}
