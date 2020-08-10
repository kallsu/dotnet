using Microsoft.EntityFrameworkCore;
using Azure.Web.Api.Models.Entities;

namespace Azure.Web.Api.Datalayer.EntityConfiguration
{
    public class BaseEntityConfiguration<T> where T : BaseEntity 
    {
        public static void OnModelCreating(ModelBuilder builder)
        {
            var sequenceName = $"{nameof(T).ToLower()}_seq";

             builder.HasSequence<long>(sequenceName)
                   .StartsAt(1)
                   .IncrementsBy(1);

            builder.Entity<T>()
                   .Property(o => o.Id)
                   .HasDefaultValueSql($"nextval('{sequenceName}')");

        }
    }
}