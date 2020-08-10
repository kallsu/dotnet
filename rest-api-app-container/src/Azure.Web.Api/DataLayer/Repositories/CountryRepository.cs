using Azure.Web.Api.Datalayer.Context;
using Azure.Web.Api.Models.Entities;

namespace Azure.Web.Api.DataLayer.Repositories
{
    public class CountryRepository : BaseRepository<Country>
    {
        public CountryRepository(MyDbContext dbContext) : base(dbContext)
        {
        }
    }
}