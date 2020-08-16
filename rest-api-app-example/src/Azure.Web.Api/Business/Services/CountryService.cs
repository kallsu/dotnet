using System.Threading.Tasks;
using Azure.Web.Api.DataLayer.Repositories;
using Azure.Web.Api.Dtos;
using Azure.Web.Api.Models.Entities;

namespace Azure.Web.Api.Business.Services
{
    public class CountryService : BaseService<Country>
    {
        public CountryService(CountryRepository repository) : base(repository)
        {
        }

        internal async Task<Country> CreateAsync(InsertCountryDto input)
        {
            var newCountry = new Country {
                Code = input.CountryCode,
                Name = input.CountryName
            };

            return await _repository.InsertAsync(newCountry);
        }
    }
}
