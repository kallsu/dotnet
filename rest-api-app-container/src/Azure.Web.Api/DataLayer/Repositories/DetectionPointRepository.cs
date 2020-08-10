using Azure.Web.Api.Datalayer.Context;
using Azure.Web.Api.Models.Entities;

namespace Azure.Web.Api.DataLayer.Repositories
{
    public class DetectionPointRepository: BaseRepository<DetectionPoint>
    {
        public DetectionPointRepository(MyDbContext dbContext) : base(dbContext)
        {
        }
    }
}