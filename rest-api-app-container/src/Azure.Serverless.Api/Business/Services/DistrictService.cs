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
        private readonly BaseRepository<Country> _countryRepository;

        public DistrictService(DistrictRepository repository, CountryRepository countryRepository) : base(repository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<District> CreateAsync(InsertDistrictDto input)
        {
            var country = await _countryRepository.GetByExpression(p => p.Code.Equals(input.CountryCode));

            if (country == null)
            {
                throw new MyAppException
                {
                    ErrorCode = MyCustomErrorCodes.COUNTRY_NOT_FOUND
                };
            }

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
