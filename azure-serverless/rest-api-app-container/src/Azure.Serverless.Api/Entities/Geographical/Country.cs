using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Azure.Serverless.Api.Entities
{
    [Table("countries")]
    public class Country : BaseEntity
    {
        [Required]
        public string Code { get; set; }

        [Required]
        [Column("Name", TypeName = "varchar(150)")]
        public string Name { get; set; }

        [ForeignKey("CountryId")]
        public List<District> districs { get; set; }
    }
}