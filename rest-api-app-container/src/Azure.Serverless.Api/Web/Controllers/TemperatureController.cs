using Microsoft.Extensions.Logging;

namespace src.Azure.Serverless.Api.Controllers
{
    public class TemperatureController : MyCopiedBaseController
    {
        public TemperatureController(ILogger<MyCopiedBaseController> logger) : base(logger)
        {
        }

        
    }
}