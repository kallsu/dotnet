using Azure.Web.Api.Datalayer.Context;
using Azure.Web.Api.Models.Entities;

namespace src.Azure.Serverless.Api.DataLayer.Repositories
{
    public class DetectionPointRepository: BaseRepository<DetectionPoint>
    {
        public DetectionPointRepository(MyDbContext dbContext) : base(dbContext)
        {
        }
    }
}