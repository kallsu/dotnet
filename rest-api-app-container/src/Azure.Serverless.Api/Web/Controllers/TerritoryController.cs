using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Web.Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Azure.Web.Api.Business.Managers;
using Azure.Web.Api.Dtos;
using Azure.Web.Api.Model.Dtos;

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
        public async Task<IActionResult> AddCountry([FromBody] InsertCountryDto input)
        {
            var result = await _manager.AddCountryAsync(input);

            return Ok(result);
        }

        [HttpGet("/district")]
        [ProducesResponseType(typeof(IEnumerable<District>), 200)]
        public async Task<IActionResult> GetDistricts([FromQuery] string countryId)
        {
            var results = await _manager.GetDistrictsAsync(countryId);

            return Ok(results);
        }

        [HttpPost("/district")]
        [ProducesResponseType(typeof(District), 200)]
        public async Task<IActionResult> AddDistrict([FromBody] InsertDistrictDto input)
        {
            var result = await _manager.AddDistrict(input);

            return Ok(result);
        }

        [HttpGet("/point")]
        public async Task<IActionResult> GetDetectionPoints([FromQuery] string districtId)
        {
            var results = await _manager.GetDetectionPointsAsync(districtId);

            return Ok(results);
        }

        [HttpPost("/point")]
        public async Task<IActionResult> AddPoint([FromBody] InsertDetectionPointDto input)
        {
            var result = await _manager.AddDetectionPointAsync(input);

            return Ok(result);
        }
    }
}