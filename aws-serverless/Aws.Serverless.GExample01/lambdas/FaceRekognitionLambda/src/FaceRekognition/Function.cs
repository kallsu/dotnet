using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.SystemTextJson;
using FaceRekognition.Core;
using FaceRekognition.Handlers;
using System;
using System.Threading.Tasks;

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
            Func<string, ILambdaContext, string> func = FunctionHandler;
            using(var handlerWrapper = HandlerWrapper.GetHandlerWrapper(func, new LambdaJsonSerializer()))
            using(var bootstrap = new LambdaBootstrap(handlerWrapper))
            {
                await bootstrap.RunAsync();
            }
        }

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        ///
        /// To use this handler to respond to an AWS event, reference the appropriate package from 
        /// https://github.com/aws/aws-lambda-dotnet#events
        /// and change the string input parameter to the desired event type.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string FunctionHandler(string input, ILambdaContext context)
        {
            try
            {
                var serviceProvider = ServiceProvider.Init(context);

                var fileHandler = serviceProvider.GetService<ImageHandler>();
                
                fileHandler.Process(s3Event)
                           .GetAwaiter()
                           .GetResult();
            }
            catch (Exception e)
            {
                return string.Format(Messages.FUNCTION_FAILED,
                    e.Message);
            }
            return Messages.FUNCTION_SUCCESS;
        }
    }
}
