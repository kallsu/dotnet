using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Web.Api.Models.Entities;
using Azure.Web.Api.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace src.Azure.Serverless.Api.Controllers
{
    public class TerritoryController : MyCopiedBaseController
    {
        private readonly ITerritoryManager _manager;

        public TerritoryController(ILogger<MyCopiedBaseController> logger, ITerritoryManager manager) : base(logger)
        {
            _manager = manager;
        }

        [HttpGet("/country")]
        [ProducesResponseType(typeof(IEnumerable<Country>), 200)]
        public async Task<IActionResult> GetCountries()
        {
            var results = await _manager.GetCountriesAsync();

            return Ok(results);
        }

        [HttpPost("/country")]
        [ProducesResponseType(typeof(Country), 200)]
        public Task<IActionResult> AddCountry([FromBody] InsertCountry input)
        {
            var result = _manager.AddCountry(input);

            return Ok(result);
        }

        [HttpGet("/district")]
        [ProducesResponseType(typeof(IEnumerable<District>), 200)]
        public Task<IActionResult> GetDistricts([FromQuery] string countryId)
        {
            var results = _manager.GetDistricts(countryId);

            return Ok(results);
        }

        [HttpPost("/district")]
        [ProducesResponseType(typeof(District), 200)]
        public Task<IActionResult> AddDistrict([FromBody] InsertDistrict input)
        {
            var result = _manager.AddDistrict(input);

            return Ok(result);
        }

        [HttpGet("/point")]
        public Task<IActionResult> GetDetectionPoints() { }

        [HttpPost("/point")]
        public Task<IActionResult> AddPoint() { }
    }
}