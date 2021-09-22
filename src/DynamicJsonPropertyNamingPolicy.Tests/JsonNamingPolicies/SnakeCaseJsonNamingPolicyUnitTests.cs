using System;
using System.Text.Json;
using DynamicJsonPropertyNamingPolicy.JsonNamingPolicies;
using Xunit;

namespace DynamicJsonPropertyNamingPolicy.Tests.JsonNamingPolicies
{
    public class SnakeCaseJsonNamingPolicyUnitTests
    {
        [Fact]
        public void TestSnakeCaseConversionThrowsOnNullInput()
        {
            // Arrange
            SnakeCaseJsonNamingPolicy policy = new();

            // Act
            ArgumentNullException actual = Assert.Throws<ArgumentNullException>(() => policy.ConvertName(null));

            // Assert
            Assert.NotNull(actual);
        }

        [Theory]
        [InlineData("snake_case")]
        [InlineData("snakeCase")]
        [InlineData("SnakeCase")]
        public void TestSnakeCaseConversion(string input)
        {
            // Arrange
            const string expected = "snake_case";
            SnakeCaseJsonNamingPolicy policy = new();

            // Act
            string actual = policy.ConvertName(input);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
