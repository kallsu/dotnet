using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;
using NpgsqlTypes;

namespace Azure.Web.Api.Models.Entities
{
    [Table("detection_points")]
    public class DetectionPoint : BaseEntity
    {
        [Required]
        public long DistrictId { get; set; }

        [ForeignKey("DistrictId")]
        public District District { get; set; }

        [Required]
        public string Code { get; set; }

        [Column(TypeName="point")]
        public NpgsqlPoint GeoLocation { get; set; }

        public List<Temperature> Temperatures {get;set;}
    }
}