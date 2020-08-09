using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Azure.Serverless.Api.Entities;
using Azure.Serverless.Api.Datalayer.EntityConfiguration;

namespace Azure.Serverless.Api.Datalayer.Context
{
    public class MyDbContext : DbContext
    {
        private readonly ILogger<MyDbContext> _logger;

        public DbSet<Country> Countries {get;set;} 
        public DbSet<District> Districts {get;set;} 
        public DbSet<DetectionPoint> DetectionPoints {get;set;} 
        public DbSet<Temperature> Temperatures {get;set;} 

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
            try
            {
                _logger = this.GetService<ILogger<MyDbContext>>();
            }
            catch { }
        }

        internal MyDbContext(DbContextOptions options, ILogger<MyDbContext> logger)
            : base(options)
        {
            _logger = logger;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (System.Exception sqlException)
            {
                _logger.LogError(sqlException.HResult, sqlException, $"Sql exception: {sqlException.Message}");
                throw sqlException;
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            CountryConfiguration.OnModelCreating(builder);
            DistrictConfiguration.OnModelCreating(builder);
            DetectionPointConfiguration.OnModelCreating(builder);
        }
    }
}