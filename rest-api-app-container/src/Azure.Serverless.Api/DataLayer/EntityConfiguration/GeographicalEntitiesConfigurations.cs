using Azure.Web.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Azure.Web.Api.Datalayer.EntityConfiguration
{
    public class CountryConfiguration : BaseEntityConfiguration<Country>
    {
        public new static void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Country>().HasIndex(p => p.Code).IsUnique();
        }
    }

    public class DistrictConfiguration : BaseEntityConfiguration<District>
    {
        public new static void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<District>().HasIndex(p => p.Code).IsUnique();
        }
    }

    public class DetectionPointConfiguration : BaseEntityConfiguration<DetectionPoint>
    {
        public new static void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DetectionPoint>().HasIndex(p => p.Code).IsUnique();
        }
    }
}