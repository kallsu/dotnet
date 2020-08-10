namespace Azure.Web.Api.Dtos
{
    public class InsertDetectionPointDto
    {
        public string DistrictCode { get; set; }

        public string PointCode { get; set; }

        public double? latitudo { get; set; }

        public double? longitudo { get; set; }
    }
}