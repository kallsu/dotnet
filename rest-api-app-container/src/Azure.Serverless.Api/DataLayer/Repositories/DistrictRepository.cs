using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Web.Api.Datalayer.Context;
using Azure.Web.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using src.Azure.Serverless.Api.DataLayer.Repositories;

namespace Azure.Web.Api.DataLayer.Repositories
{
    public class DistrictRepository : BaseRepository<District>
    {
        public DistrictRepository(MyDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<District>> GetDistrictsAsync(long countryId)
        {
            return await _dbContext.Set<District>()
                                        .Where(p => p.CountryId == countryId)
                                        .OrderBy(p => p.Name)
                                        .ToListAsync();
        }
    }
}
