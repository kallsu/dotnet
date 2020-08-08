using System.Net;
using System.Threading.Tasks;
using Azure.Serverless.Api.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using src.Azure.Serverless.Api.Commons;

namespace src.Azure.Serverless.Api.Filters
{
    public class TokenFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!await IsExistedSession(context))
            {

                context.Result = new UnauthorizedObjectResult(new[]
                {
                    new MyError(){  MyCustomErrorCode = MyCustomErrorCodes.SESSION_EXPIRED }
                    
                }) { StatusCode = (int) HttpStatusCode.Unauthorized, DeclaredType = typeof(MyError[]) };
            }
            else
            {
                await next();
            }
        }

        private async Task<bool> IsExistedSession(ActionExecutingContext context)
        {
            //Anonymous case
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                return true;
            }

            if (!context.HttpContext.Request.Headers["Authorization"][0].StartsWith("Bearer "))
            {
                return false;
            }

            var sessionToken = context.HttpContext.Request.Headers["Authorization"][0].Substring("Bearer ".Length);

            // Add here the custom session management
            return await MyCustomSessionManagement(sessionToken);
        }

        private async Task<bool> MyCustomSessionManagement(string sessionToken) 
        {
            // session managment very easy and simpliest.

            if(string.IsNullOrEmpty(sessionToken))
                return false;

            return true;
        }
    }
}