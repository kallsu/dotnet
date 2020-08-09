using System.Threading.Tasks;
using Azure.Web.Api.Commons;
using Azure.Web.Api.Exception;
using Azure.Web.Api.Model.Dtos;
using Azure.Web.Api.Models.Entities;
using src.Azure.Serverless.Api.DataLayer.Repositories;

namespace Azure.Web.Api.Business.Services
{
    public class DistrictService : BaseService<District>
    {
        public DistrictService(DistrictRepository repository) : base(repository)
        {
        }

        public async Task<District> CreateAsync(InsertDistrictDto input, Country country)
        {
            var newDistrict = new District {
                Code = input.DistrictCode,
                Name = input.DistrictName,
                CountryId = country.Id,
                Country = country                
            };

            return await _repository.InsertAsync(newDistrict);
        }
    }
}
