using Microsoft.Extensions.Configuration;
using System.IO;

namespace Dtac.Ev.Lambdas
{
    public class LambdaConfiguration : ILambdaConfiguration
    {
        public static IConfigurationRoot Configuration => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddEnvironmentVariables()
            .Build();
        IConfigurationRoot ILambdaConfiguration.Configuration => Configuration;
    }
}
