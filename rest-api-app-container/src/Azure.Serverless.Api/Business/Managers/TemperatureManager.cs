using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Web.Api.Business.Services;
using Azure.Web.Api.Commons;
using Azure.Web.Api.Exception;
using Azure.Web.Api.Models.Entities;

namespace Azure.Web.Api.Business.Managers
{
    public class TemperatureManager : ITemperatureManager
    {
        private readonly TemperatureService _service;

        public TemperatureManager(TemperatureService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<Temperature>> GetTemperaturesByDetectionPoint(string pointId)
        {
            if (!long.TryParse(pointId, out long id))
            {
                throw new MyAppException
                {
                    ErrorCode = MyCustomErrorCodes.DISTRICT_ID_UNPARSABLE
                };
            }

            return await _service.GetByDetectionPointId(id);
        }

        public async Task<IEnumerable<Temperature>> GetTemperaturesByDistrict(string districtId)
        {
            if (!long.TryParse(districtId, out long id))
            {
                throw new MyAppException
                {
                    ErrorCode = MyCustomErrorCodes.DISTRICT_ID_UNPARSABLE
                };
            }

            return await _service.GetByDistrictId(id);
        }
    }
}
