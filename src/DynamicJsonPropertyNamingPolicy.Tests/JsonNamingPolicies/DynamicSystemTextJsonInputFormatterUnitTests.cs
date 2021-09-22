using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using DynamicJsonPropertyNamingPolicy.JsonNamingPolicies;
using Xunit;

namespace DynamicJsonPropertyNamingPolicy.Tests.JsonNamingPolicies
{
    public class DynamicSystemTextJsonInputFormatterUnitTests
    {
        private class SampleType
        {
            public string NamingPolicy { get; set; }
        }

        [Theory]
        [InlineData("snake", "naming_policy")]
        [InlineData("pascal", "NamingPolicy")]
        [InlineData("camel", "namingPolicy")]
        public async void TestReadRequestBodyAsync(string policyName, string propertyName)
        {
            // Arrange
            string requestBody = $"{{\"{propertyName}\":\"{policyName}\"}}";
            Encoding encoding = Encoding.UTF8;
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.TryAddSingleton<ILogger<SystemTextJsonInputFormatter>>(
                new NullLogger<SystemTextJsonInputFormatter>());
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateOnBuild = true,
                ValidateScopes = true
            });
            DefaultHttpContext httpContext = new()
            {
                Request = { Body = new MemoryStream(encoding.GetBytes(requestBody)) }
            };
            httpContext.Request.Headers.Add("json-naming-strategy", policyName);
            httpContext.RequestServices = serviceProvider;
            InputFormatterContext context = new(
                httpContext,
                nameof(SampleType),
                new ModelStateDictionary(),
                new EmptyModelMetadataProvider().GetMetadataForType(typeof(SampleType)),
                (stream, encoding) => new StreamReader(stream, encoding));
            DynamicSystemTextJsonInputFormatter formatter = new();

            // Act
            InputFormatterResult actual = await formatter.ReadRequestBodyAsync(context, encoding);

            // Assert
            Assert.NotNull(actual.Model);
            Assert.IsType<SampleType>(actual.Model);
            Assert.Equal(policyName, ((SampleType)actual.Model).NamingPolicy);
        }
    }
}
