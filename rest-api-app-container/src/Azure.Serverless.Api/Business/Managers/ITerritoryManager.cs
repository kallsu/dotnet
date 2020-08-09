using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Web.Api.Dtos;
using Azure.Web.Api.Models.Entities;

namespace Azure.Web.Api.Business.Managers
{
    public interface ITerritoryManager
    {
        Task<IEnumerable<Country>> GetCountriesAsync();
        Task<Country> AddCountryAsync(InsertCountryDto input);

        Task<IEnumerable<District>> GetDistrictsAsync(string countryId);
        Task<District> AddDistrict(InsertDistrictDto input);

        Task<IEnumerable<DetectionPoint>> GetDetectionPointsAsync();
        Task<DetectionPoint> AddDetectionPointAsync();
    }
}
