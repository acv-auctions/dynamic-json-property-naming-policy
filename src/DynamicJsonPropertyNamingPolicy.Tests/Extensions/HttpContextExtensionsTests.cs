using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using DynamicJsonPropertyNamingPolicy.Extensions;
using Xunit;
using System;

namespace DynamicJsonPropertyNamingPolicy.Tests.Extensions
{
    public class HttpContextExtensionsTests
    {
        public static TheoryData<string, JsonSerializerOptions> NamingOptionData =>
            new()
            {
                { "snake", HttpContextExtensions._snakeCaseOptions },
                { "camel", HttpContextExtensions._camelCaseOptions },
                { "pascal", HttpContextExtensions._pascalCaseOptions },
                { "unclegeorge", HttpContextExtensions._snakeCaseOptions },
                { null, HttpContextExtensions._snakeCaseOptions }
            };

        [Theory]
        [MemberData(nameof(NamingOptionData))]
        public void GetJsonNamingPolicyTest(string headerValue, JsonSerializerOptions expected)
        {
            // Arrange
            HttpContext context = new DefaultHttpContext();
            if (headerValue != null)
            {
                context.Request.Headers["json-naming-strategy"] = headerValue;
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
                context.Request.Headers["json-naming-strategy"] = headerValue;
            }

            // Act
            JsonSerializerOptions actual = context.GetJsonSerializerOptions();

            // Assert
            Assert.Same(expected, actual);
        }
    }
}
