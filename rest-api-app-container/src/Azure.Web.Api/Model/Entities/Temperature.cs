using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Azure.Web.Api.Models.Entities
{
    [Table("temperatures")]
    public class Temperature : BaseEntity
    {
        [Required]
        public long DetectionPointId { get; set; }

        [ForeignKey("DetectionPointId")]
        public DetectionPoint DetenctionPoint { get; set; }

        [Required]
        public int CelsiusDegree { get; set; }

        public int GetFahrenheitDegree()
        {
            return (CelsiusDegree * (9 / 5)) + 32;
        }
    }
}