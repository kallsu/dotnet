using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Serverless.Api.Entities;

namespace Azure.Serverless.Api.Managers
{
    public interface ITerritoryManager
    {
        Task<IEnumerable<Country>> GetCountries();
        Task<Country> AddCountry(InsertCountry input);

        Task<IEnumerable<District>> GetDistricts(string countryId);
        Task<District> AddDistrict(InsertDistrict input);

        Task<IEnumerable<DetectionPoint>> GetDetectionPoints();
        Task<DetectionPoint> AddDetectionPoint();
    }
}
