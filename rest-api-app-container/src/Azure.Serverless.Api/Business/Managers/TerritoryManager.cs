using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Web.Api.Business.Services;
using Azure.Web.Api.Commons;
using Azure.Web.Api.Dtos;
using Azure.Web.Api.Exception;
using Azure.Web.Api.Model.Dtos;
using Azure.Web.Api.Models.Entities;

namespace Azure.Web.Api.Business.Managers
{
    public class TerritoryManager : ITerritoryManager
    {
        private readonly CountryService _countryService;
        private readonly DistrictService _districtService;
        private readonly DetectionPointService _pointService;

        public TerritoryManager(CountryService countryService, DistrictService districtService, DetectionPointService pointService)
        {
            _countryService = countryService;
            _districtService = districtService;
            _pointService = pointService;
        }

        public async Task<Country> AddCountryAsync(InsertCountryDto input)
        {
            return await _countryService.CreateAsync(input);
        }

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            return await _countryService.GetAllAsync();
        }

        public async Task<District> AddDistrict(InsertDistrictDto input)
        {
            var country = await _countryService.GetByExpression(p => p.Code.Equals(input.CountryCode));

            if (country == null)
            {
                throw new MyAppException
                {
                    ErrorCode = MyCustomErrorCodes.COUNTRY_NOT_FOUND
                };
            }

            return await _districtService.CreateAsync(input, country);
        }

        public async Task<IEnumerable<District>> GetDistrictsAsync(string countryId)
        {
            if (!long.TryParse(countryId, out long id))
            {
                throw new MyAppException
                {
                    ErrorCode = MyCustomErrorCodes.COUNTRY_ID_UNPARSABLE
                };
            }

            return _districtService.GetAllAsync<string>(
                (p => p.CountryId == id),
                (p => p.Name),
                true
            );
        }

        public async Task<DetectionPoint> AddDetectionPointAsync(InsertDetectionPointDto input)
        {
            var district = await _districtService.GetByExpression(p => p.Code.Equals(input.DistrictCode));

            if (district == null)
            {
                throw new MyAppException
                {
                    ErrorCode = MyCustomErrorCodes.DISTRICT_NOT_FOUND
                };
            }

            return await _pointService.CreateAsync(input, district);
        }

        public async Task<IEnumerable<DetectionPoint>> GetDetectionPointsAsync(string districtId)
        {
            if (!long.TryParse(districtId, out long id))
            {
                throw new MyAppException
                {
                    ErrorCode = MyCustomErrorCodes.DISTRICT_ID_UNPARSABLE
                };
            }

            return _pointService.GetAllAsync(
                (p => p.DistrictId == id),
                (p => p.Created),
                false);
        }
    }
}
