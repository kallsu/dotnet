using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.SystemTextJson;
using FaceRekognition.Core;
using FaceRekognition.Handlers;
using System;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.S3Events;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace FaceRekognition
{
    public class Function
    {
        /// <summary>
        /// The main entry point for the custom runtime.
        /// </summary>
        /// <param name="args"></param>
        private static async Task Main(string[] args)
        {
            Func<S3Event, ILambdaContext, string> func = FunctionHandler;
            using(var handlerWrapper = HandlerWrapper.GetHandlerWrapper(func, new LambdaJsonSerializer()))
            using(var bootstrap = new LambdaBootstrap(handlerWrapper))
            {
                await bootstrap.RunAsync();
            }
        }

        /// <summary>
        /// Detect any faces.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        public static string FunctionHandler(S3Event input, ILambdaContext context)
        {
            try
            {
                var serviceProvider = ServiceProvider.Init(context);

                var imageHandler = (ImageHandler) serviceProvider.GetService(typeof(ImageHandler));

                var bucketName = input.Records[0].S3.Bucket.Name;
                var fileName = input.Records[0].S3.Object.Key.Split("/").Last();
                
                var isOk = imageHandler.ProcessAsync(bucketName, fileName)
                           .GetAwaiter()
                           .GetResult();

                return isOk
                    ? Messages.FUNCTION_SUCCESS
                    : Messages.NO_FACE;
            }
            catch (Exception e)
            {
                return string.Format(Messages.FUNCTION_FAILED,
                    e.Message);
            }
        }
    }
}
