using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Web.Api.DataLayer.Repositories;
using Azure.Web.Api.Models.Entities;
using src.Azure.Serverless.Api.DataLayer.Repositories;

namespace Azure.Web.Api.Business.Services
{
    public class DistrictService : BaseService<District>
    {
        public DistrictService(BaseRepository<District> repository) : base(repository)
        {
        }

        internal async Task<IEnumerable<District>> GetAllAsync(long countryId)
        {
            var repo = _repository as DistrictRepository;

            return await repo.GetDistrictsAsync(countryId);
        }
    }
}
