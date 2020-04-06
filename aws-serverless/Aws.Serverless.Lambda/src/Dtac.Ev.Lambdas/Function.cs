using Amazon.Lambda.CloudWatchLogsEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using JsonSerializer = Amazon.Lambda.Serialization.Json.JsonSerializer;

namespace Dtac.Ev.Lambdas
{
    public class Function
    {
       public static async Task Main(string[] args)
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("AwsLambdaFunctionName")))
            {
                CreateHostBuilder(args).Build().Run();
            }
            else
            {
                // Wrap the FunctionHandler method in a form that LambdaBootstrap can work with.
                using (var handlerWrapper = HandlerWrapper.GetHandlerWrapper((Func<CloudWatchLogsEvent, ILambdaContext, string>)FunctionHandler, new JsonSerializer()))
                {
                    // Instantiate a LambdaBootstrap and run it.
                    // It will wait for invocations from AWS Lambda and call
                    // the handler function for each one.
                    using (var bootstrap = new LambdaBootstrap(handlerWrapper))
                    {
                        await bootstrap.RunAsync();
                    }
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseLibuv();
                });

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// 
        /// To use this handler to respond to an AWS event, reference the appropriate package from 
        /// https://github.com/aws/aws-lambda-dotnet#events
        /// and change the string input parameter to the desired event type.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static void FunctionHandler(CloudWatchLogsEvent triggerEvent, ILambdaContext context)
        {
            context.Logger.Log($"C# Timer trigger function executed at: {DateTime.Now}");

            try
            {
                // do something here

            }
            catch (Exception e)
            {
                context.Logger.Log(e.Message);
            }

            context.Logger.Log($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
