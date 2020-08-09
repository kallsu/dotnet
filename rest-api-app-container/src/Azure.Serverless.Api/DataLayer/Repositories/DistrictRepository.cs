using Azure.Web.Api.Datalayer.Context;
using Azure.Web.Api.Models.Entities;

namespace src.Azure.Serverless.Api.DataLayer.Repositories
{
    public class DistrictRepository : BaseRepository<District>
    {
        public DistrictRepository(MyDbContext dbContext) : base(dbContext)
        {
        }
    }
}