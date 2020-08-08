using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Azure.Serverless.Api.Entities
{
    [Table("districts")]
    public class District : BaseEntity
    {
        [Required]
        public long CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        [Column("Name", TypeName = "varchar(200)")]
        public string Name { get; set; }

        [ForeignKey("DistrictId")]
        public List<DetenctionPoint> Points { get; set; }
    }
}