using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Web.Api.DataLayer.Repositories;
using Azure.Web.Api.Models.Entities;

namespace Azure.Web.Api.Business.Services
{
    public class TemperatureService : BaseService<Temperature>
    {
        public TemperatureService(TemperatureRepository repository) : base(repository)
        {
        }

        public async Task<IEnumerable<Temperature>> GetByDistrictId(long id)
        {
            var repo = _repository as TemperatureRepository;

            return await repo.GetByDistrictId(id);
        }

        public async Task<IEnumerable<Temperature>> GetByDetectionPointId(long id)
        {
            var repo = _repository as TemperatureRepository;

            return await repo.GetByDetectionPoint(id);
        }
    }
}
