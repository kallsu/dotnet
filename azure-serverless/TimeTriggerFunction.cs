using System;
using Azure.Serverless.Core;
using Azure.Serverless.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

/// 
/// Develop Azure Function : https://docs.microsoft.com/en-us/azure/azure-functions/functions-dotnet-class-library
///
/// Azure Function Best-Practice : https://docs.microsoft.com/en-us/azure/azure-functions/functions-best-practices
//
/// CRON Expression : https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-timer?tabs=csharp#ncrontab-expressions
///
namespace Azure.Serverless {

    public static class TimeTriggerFunction {

        [FunctionName ("TimeTriggerFunction")]
        public static void Run (
            [TimerTrigger ("0 33 3 * * *")] TimerInfo myTimer, 
            [Queue ("101functionsqueue", Connection = "AzureWebJobsStorage")] ICollector<LogRecord> queueCollector,
            ILogger logger,
            ExecutionContext context) {

            // Timing is in UTC
            logger.LogInformation ($"C# Timer trigger function executed at: {DateTime.Now}");

            try {
                // Initialize the Service Provider
                var serviceProvider = AzureFunctionServiceProvider.Create (context, logger);

                // Do something here ... Sync or Async ?
                // https://docs.microsoft.com/en-us/azure/azure-functions/functions-best-practices#scalability-best-practices

                // record the success here
                queueCollector.Add (
                    new LogRecord {
                        LogLevel = (int) LogLevelEnum.USEFUL_LOG,
                            Text = "OK. Function execution ends."
                    });

            } catch (Exception e) {
                logger.LogError (e, e.Message);

                // ops ... there is an error
                queueCollector.Add (
                    new LogRecord {
                        LogLevel = (int) LogLevelEnum.ERROR,
                            Text = e.Message
                    });
            }

            // Bye Bye !!!
            logger.LogInformation ("That's all folks!");
        }
    }
}