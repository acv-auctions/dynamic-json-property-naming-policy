using System;
using System.Text;
using System.Threading.Tasks;
using DynamicJsonPropertyNamingPolicy.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DynamicJsonPropertyNamingPolicy.JsonNamingPolicies
{
    /// <summary>
    /// A <see cref="Microsoft.AspNetCore.Mvc.Formatters.TextInputFormatter" /> that uses <see cref="DynamicJsonPropertyNamingPolicy.Extensions.HttpContextExtensions.GetJsonNamingPolicy" /> to determine what <see cref="System.Text.Json.JsonSerializerOptions.PropertyNamingPolicy" /> to use.
    /// </summary>
    public class DynamicSystemTextJsonInputFormatter : TextInputFormatter
    {
        /// <summary>
        /// Default constructor that handles <c>Content-type: application/json; charset=utf-8</c>
        /// </summary>
        public DynamicSystemTextJsonInputFormatter()
        {
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedMediaTypes.Add("application/json");
        }

        /// <inheritdoc />
        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            HttpContext httpContext = context.HttpContext;

            if (httpContext.RequestAborted.IsCancellationRequested)
            {
                return InputFormatterResult.NoValue();
            }

            ILogger<SystemTextJsonInputFormatter> inputLogger = httpContext.RequestServices.GetRequiredService<ILogger<SystemTextJsonInputFormatter>>();
            JsonOptions options = new();
            options.JsonSerializerOptions.PropertyNamingPolicy = httpContext.GetJsonNamingPolicy();

            TextInputFormatter formatter = new SystemTextJsonInputFormatter(options, inputLogger);
            InputFormatterResult result = await formatter.ReadRequestBodyAsync(context, encoding);

            return result;
        }
    }
}
