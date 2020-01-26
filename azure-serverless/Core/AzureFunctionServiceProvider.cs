using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Azure.Serverless.Core
{

    public static class AzureFunctionServiceProvider
    {
        internal static IServiceProvider Create(ExecutionContext context,
                                                 ILogger logger)
        {
            var config = ReadConfiguration(context);
            var serviceCollection = new ServiceCollection();


            return serviceCollection.BuildServiceProvider();
        }

        private static IConfigurationRoot ReadConfiguration(ExecutionContext context)
        {
            return new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}