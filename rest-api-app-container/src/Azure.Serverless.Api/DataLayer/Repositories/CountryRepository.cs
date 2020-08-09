using System;
using Azure.Web.Api.Datalayer.Context;
using Azure.Web.Api.Models.Entities;
using src.Azure.Serverless.Api.DataLayer.Repositories;

namespace Azure.Web.Api.DataLayer.Repositories
{
    public class CountryRepository : BaseRepository<Country>
    {
        public CountryRepository(MyDbContext dbContext) : base(dbContext)
        {
        }
    }
}
