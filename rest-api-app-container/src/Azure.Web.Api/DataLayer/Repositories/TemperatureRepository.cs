using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Web.Api.Datalayer.Context;
using Azure.Web.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Azure.Web.Api.DataLayer.Repositories
{
    public class TemperatureRepository : BaseRepository<Temperature>
    {
        public TemperatureRepository(MyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Temperature>> GetByDistrictId(long id)
        {
            return await _dbContext.Temperatures.AsQueryable()
                    .Include(p => p.DetenctionPoint)
                    .ThenInclude(p => p.District)
                    .Where(c => c.DetenctionPoint.District.Id == id)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Temperature>> GetByDetectionPoint(long id)
        {
            return await _dbContext.Temperatures.AsQueryable()
                    .Include(p => p.DetenctionPoint)
                    .Where(c => c.DetenctionPoint.Id == id)
                    .ToListAsync();
        }
    }
}
