using Azure.Serverless.Web.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Azure.Serverless.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ForecastController : ControllerBase
    {
        private readonly WeatherForecastManager _manager;

        public ForecastController() {
            this._manager = new WeatherForecastManager();
        }

        [HttpGet]
        public async Task<IActionResult> GetWeatherForecasts() 
        {
            var results = await _manager.GetForecasts();

            return Ok(results);
        }
    }
}