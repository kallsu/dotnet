using Azure.Web.Api.Models.Entities;
using src.Azure.Serverless.Api.DataLayer.Repositories;

namespace Azure.Web.Api.Business.Services
{
    public class CountryService : BaseService<Country>
    {
        public CountryService(BaseRepository<Country> repository) : base(repository)
        {
        }
    }
}
