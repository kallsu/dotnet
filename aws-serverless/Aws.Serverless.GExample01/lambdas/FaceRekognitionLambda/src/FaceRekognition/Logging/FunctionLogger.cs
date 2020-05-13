using Amazon.Lambda.Core;

namespace FaceRekognition.Logging
{

    public class FunctionLogger
    {
        private readonly ILambdaContext _context;

        public FunctionLogger(ILambdaContext context)
        {
            this._context = context;
        }

        public void Log(string message, params string[] arguments)
        {
            var logMessage = message;

            if (arguments != null && arguments.Length > 0)
            {
                string.Format(message, arguments);
            }

            _context.Logger.Log(logMessage);
        }
    }
}