using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Web.Api.Business.Managers;
using Azure.Web.Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace src.Azure.Serverless.Api.Controllers
{
    public class TemperatureController : MyCopiedBaseController
    {
        private readonly ITemperatureManager _manager;

        public TemperatureController(ILogger<MyCopiedBaseController> logger, ITemperatureManager manager) : base(logger)
        {
            _manager = manager;
        }

        [HttpGet("/temperature")]
        public async Task<IActionResult> GetDetectionPoints([FromQuery] string districtId, [FromQuery] string pointId)
        {
            IEnumerable<Temperature> results = null;

            if(!string.IsNullOrEmpty(pointId))
            {
                results = await _manager.GetTemperaturesByDetectionPoint(pointId);
            }
            else if(!string.IsNullOrEmpty(districtId))
            {
                results = await _manager.GetTemperaturesByDistrict(districtId);
            }
            else {
                return BadRequest("No district or point specified");
            }

            return Ok(results);
        }
    }
}