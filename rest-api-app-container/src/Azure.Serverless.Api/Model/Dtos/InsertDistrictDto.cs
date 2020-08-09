using System;

namespace Azure.Web.Api.Model.Dtos
{
    public class InsertDistrictDto
    {
        public string CountryCode { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
    }
}
