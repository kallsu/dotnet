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

        public TerritoryManager(CountryService countryService, DistrictService districtService)
        {
            _countryService = countryService;
            _districtService = districtService;
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
            return await _districtService.CreateAsync(input);
        }

        public async Task<IEnumerable<District>> GetDistrictsAsync(string countryId)
        {
            if(!long.TryParse(countryId, out long id)) {
                throw new MyAppException {
                    ErrorCode = MyCustomErrorCodes.COUNTRY_ID_UNPARSABLE
                };
            }

            var country = await _countryService.GetByIdAsync(id);

            if (country == null)
            {
                throw new MyAppException
                {
                    ErrorCode = MyCustomErrorCodes.COUNTRY_NOT_FOUND
                };
            }

            return _districtService.GetAllAsync<string>(
                (p => p.CountryId == id),
                (p => p.Name),
                true
            );
        }

        public Task<DetectionPoint> AddDetectionPointAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<DetectionPoint>> GetDetectionPointsAsync()
        {
            throw new System.NotImplementedException();
        }

        
    }
}
