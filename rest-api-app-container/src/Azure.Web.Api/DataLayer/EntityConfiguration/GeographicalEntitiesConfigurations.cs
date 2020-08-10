using Azure.Web.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Azure.Web.Api.Datalayer.EntityConfiguration
{
    public class CountryConfiguration : BaseEntityConfiguration<Country>
    {
        public new static void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Country>().HasIndex(p => p.Code).IsUnique();

            builder.Entity<Country>().HasMany(p => p.Districs).WithOne(c => c.Country);
        }
    }

    public class DistrictConfiguration : BaseEntityConfiguration<District>
    {
        public new static void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<District>().HasIndex(p => p.Code).IsUnique();

            builder.Entity<District>().HasMany(p => p.Points).WithOne(c => c.District);
        }
    }

    public class DetectionPointConfiguration : BaseEntityConfiguration<DetectionPoint>
    {
        public new static void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DetectionPoint>().HasIndex(p => p.Code).IsUnique();

            builder.Entity<DetectionPoint>().HasMany(p => p.Temperatures).WithOne(c => c.DetenctionPoint);
        }
    }
}