using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Web.Api.Models.Entities;

namespace Azure.Web.Api.Business.Managers
{
    public interface ITemperatureManager
    {
        Task<IEnumerable<Temperature>> GetTemperaturesByDistrict(string districtId);

        Task<IEnumerable<Temperature>> GetTemperaturesByDetectionPoint(string districtId);
    }
}
