using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DynamicJsonPropertyNamingPolicy.Extensions;
using System;
using Microsoft.AspNetCore.Http;

namespace DynamicJsonPropertyNamingPolicy.JsonNamingPolicies
{
    /// <summary>
    /// A <cref>Microsoft.AspNetCore.Mvc.Formatters.TextInputFormatter</cref> that uses <cref>DynamicJsonPropertyNamingPolicy.Extensions.HttpContextExtensions.GetJsonNamingPolicy</cref> to determine what <cref>System.Text.Json.JsonSerializerOptions.PropertyNamingPolicy</cref> to use.
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
