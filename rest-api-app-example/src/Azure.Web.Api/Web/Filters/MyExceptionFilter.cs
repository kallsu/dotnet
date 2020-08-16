using Azure.Web.Api.Exception;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Azure.Web.Api.Filters
{
    public class MyExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<MyExceptionFilter> _logger;
        private readonly IWebHostEnvironment _environment;

        public MyExceptionFilter(ILogger<MyExceptionFilter> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public void OnException(ExceptionContext context)
        {
            var appException = context.Exception as MyAppException;
            var errorCode = appException?.ErrorCode ?? 0;

            // create my error for custom solution
            var cutomError = new MyError
            {
                MyCustomErrorCode = errorCode,
                FieldName = string.IsNullOrEmpty(appException.Field) ? null : appException.Field
            };

            // custom error management
            if (errorCode >= 400 && errorCode <= 499)
            {
                // all this codes return a 400 as HTTP response

                context.Result = new ObjectResult(cutomError)
                {
                    StatusCode = 400,
                    DeclaredType = typeof(MyError)
                };
            }
            else
            {
                // all the other codes return a 500 as HTTP response
                context.Result = new ObjectResult(cutomError)
                {
                    StatusCode = 500,
                    DeclaredType = typeof(MyError)
                };
            }
        }
    }
}