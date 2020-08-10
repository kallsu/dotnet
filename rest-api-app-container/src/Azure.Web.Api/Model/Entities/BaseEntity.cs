using System;
using System.ComponentModel.DataAnnotations;

namespace Azure.Web.Api.Models.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        public DateTime Updated { get; set; }
    }
}