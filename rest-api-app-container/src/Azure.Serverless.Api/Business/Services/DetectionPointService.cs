using System.Threading.Tasks;
using Azure.Web.Api.Dtos;
using Azure.Web.Api.Models.Entities;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using src.Azure.Serverless.Api.DataLayer.Repositories;

namespace Azure.Web.Api.Business.Services
{
    public class DetectionPointService : BaseService<DetectionPoint>
    {
        public DetectionPointService(DetectionPointRepository repository) : base(repository)
        {
        }

        internal async Task<DetectionPoint> CreateAsync(InsertDetectionPointDto input, District district)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            
            var newPoint = new DetectionPoint {
                Code = input.PointCode,
                District = district,
                DistrictId = district.Id
            };

            if(input.latitudo.HasValue && input.longitudo.HasValue) {
                newPoint.GeoLocation = geometryFactory.CreatePoint(
                    new Coordinate(
                        input.longitudo.Value, 
                        input.latitudo.Value));
            }

            return await _repository.InsertAsync(newPoint);
        }
    }
}