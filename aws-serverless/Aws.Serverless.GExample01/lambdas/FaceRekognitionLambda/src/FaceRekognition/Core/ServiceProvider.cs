using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Lambda.Core;
using Amazon.Runtime;
using Amazon.Rekognition;
using FaceRekognition.Handlers;
using FaceRekognition.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FaceRekognition.Core
{
    internal static class ServiceProvider
    {
        public static IServiceProvider Init(ILambdaContext context)
        {
            var debugLogger = new FunctionLogger(context);

            debugLogger.Log("Init.");

            IServiceCollection services = new ServiceCollection();

            // logging
            debugLogger.Log("Logging. LOAD");
            services.AddScoped(option => new FunctionLogger(context));

            debugLogger.Log("Logging. DONE");

            // Region Code
            var regionCode = Environment.GetEnvironmentVariable("RegionCode");
            var ssmPath = Environment.GetEnvironmentVariable("SSMVariablesPath");

            debugLogger.Log($"Region Code : {regionCode}");
            debugLogger.Log($"SSM Path : {ssmPath}");

            debugLogger.Log("SSM Configuration. LOAD");

            // SSM Parameters
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddSystemsManager(ssmPath, new AWSOptions { Region = RegionEndpoint.GetBySystemName(regionCode) });
            var configurations = configurationBuilder.Build();

            // Aws Credentials
            var awsCredentials =
                new BasicAWSCredentials(
                    accessKey: configurations.GetSection("Aws:Credentials:AccessKey").Value,
                    secretKey: configurations.GetSection("Aws:Credentials:SecretId").Value);

            debugLogger.Log("SSM Configuration. DONE");

            // face rekognition
            debugLogger.Log("Face Recognition. LOAD");

            services.AddScoped(o =>
                new AmazonRekognitionClient(awsCredentials, RegionEndpoint.GetBySystemName(regionCode)));

            debugLogger.Log("Face Recognition. DONE");

            // Function DI
            services.AddScoped<ImageHandler>();

            debugLogger.Log("Init. DONE.");

            return services.BuildServiceProvider();
        }
    }
}