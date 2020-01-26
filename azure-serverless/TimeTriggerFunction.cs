using System;
using Azure.Serverless.Core;
using azure_serverless.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

/// 
/// Develop Azure Function : https://docs.microsoft.com/en-us/azure/azure-functions/functions-dotnet-class-library
///
/// CRON Expression : https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-timer?tabs=csharp#ncrontab-expressions
///
namespace Azure.Serverless
{
    public static class TimeTriggerFunction
    {
        [FunctionName("TimeTriggerFunction")]
        public static void Run([TimerTrigger("0 33 3 * * *")]TimerInfo myTimer,
            ILogger logger,
            ExecutionContext context)
        {
            // Timing is in UTC
            logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            try
            {
                // Initialize the Service Provider
                var serviceProvider = AzureFunctionServiceProvider.Create(context, logger);

                // Do something here ...


                // send log message
                var logService = (LogService)serviceProvider.GetService(typeof(LogService));

                // Sync or Async ??????
                logService.SendLogMessage();
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);

                // send a log here
            }

            // Bye Bye !!!
            logger.LogInformation("That's all folks!");
        }
    }
}
