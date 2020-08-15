using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Web.Api.Datalayer.Context;
using Azure.Web.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace Azure.Web.Api.DataLayer.MockData
{
    public static class MockDataIntializer
    {
        public static async Task InitDatabase(MyDbContext dbContext)
        {
            // apply migrations
            dbContext.Database.Migrate();

            // delete all records
            dbContext.Temperatures.RemoveRange(await dbContext.Temperatures.ToListAsync());
            dbContext.DetectionPoints.RemoveRange(await dbContext.DetectionPoints.ToListAsync());
            dbContext.Districts.RemoveRange(await dbContext.Districts.ToListAsync());
            dbContext.Countries.RemoveRange(await dbContext.Countries.ToListAsync());

            // countries
            var countries = new List<Country>(){
                new Country {
                    Code = "TH",
                    Name = "Thailand"
                },
                new Country {
                    Code = "IT",
                    Name = "Italy"
                },
            };

            await dbContext.Countries.AddRangeAsync(countries);
            await dbContext.SaveChangesAsync();

            // district
            var th = await dbContext.Countries.FirstOrDefaultAsync(c => c.Code.Equals("TH"));

            // Thailand
            var thaiDistrict = new List<District>() {
                new District {
                    Code = "BKK",
                    Name = "Bangkok",
                    CountryId = th.Id
                },
                new District {
                    Code = "SPN",
                    Name = "Samut Prakan",
                    CountryId = th.Id
                },
                new District {
                    Code = "CHM",
                    Name = "Chang Mai",
                    CountryId = th.Id
                }
            };

            await dbContext.Districts.AddRangeAsync(thaiDistrict);
            await dbContext.SaveChangesAsync();

            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            // Points
            var points = new List<DetectionPoint>() {
                new DetectionPoint {
                    Code = "BKK-01",
                    DistrictId = (await dbContext.Districts.FirstOrDefaultAsync(d => d.Code.Equals("BKK"))).Id,
                    GeoLocation = geometryFactory.CreatePoint(new Coordinate(100.5561358D, 13.7221077D))
                },

                new DetectionPoint {
                    Code = "SPN-01",
                    DistrictId = (await dbContext.Districts.FirstOrDefaultAsync(d => d.Code.Equals("SPN"))).Id,
                    GeoLocation = geometryFactory.CreatePoint(new Coordinate(100.6774227D, 13.6446509D))
                },

                new DetectionPoint {
                    Code = "SPN-02",
                    DistrictId = (await dbContext.Districts.FirstOrDefaultAsync(d => d.Code.Equals("SPN"))).Id,
                    GeoLocation = geometryFactory.CreatePoint(new Coordinate(100.6208533D, 13.5392202D))
                },

                new DetectionPoint {
                    Code = "CHM-01",
                    DistrictId = (await dbContext.Districts.FirstOrDefaultAsync(d => d.Code.Equals("CHM"))).Id,
                    GeoLocation = geometryFactory.CreatePoint(new Coordinate(98.8864357D, 18.7943954D))
                },
            };

            await dbContext.DetectionPoints.AddRangeAsync(points);
            await dbContext.SaveChangesAsync();

            // Here the temperature
            var temps = new List<Temperature>()
            {
                new Temperature {
                    CelsiusDegree = 21,
                    DetectionPointId = (await dbContext.DetectionPoints.FirstOrDefaultAsync(p => p.Code.Equals("CHM-01"))).Id
                },
                new Temperature {
                    CelsiusDegree = 18,
                    DetectionPointId = (await dbContext.DetectionPoints.FirstOrDefaultAsync(p => p.Code.Equals("CHM-01"))).Id
                },
                new Temperature {
                    CelsiusDegree = 26,
                    DetectionPointId = (await dbContext.DetectionPoints.FirstOrDefaultAsync(p => p.Code.Equals("CHM-01"))).Id
                },
                new Temperature {
                    CelsiusDegree = 32,
                    DetectionPointId = (await dbContext.DetectionPoints.FirstOrDefaultAsync(p => p.Code.Equals("BKK-01"))).Id
                },
                new Temperature {
                    CelsiusDegree = 34,
                    DetectionPointId = (await dbContext.DetectionPoints.FirstOrDefaultAsync(p => p.Code.Equals("BKK-01"))).Id
                },
                new Temperature {
                    CelsiusDegree = 31,
                    DetectionPointId = (await dbContext.DetectionPoints.FirstOrDefaultAsync(p => p.Code.Equals("SPN-01"))).Id
                },
                new Temperature {
                    CelsiusDegree = 29,
                    DetectionPointId = (await dbContext.DetectionPoints.FirstOrDefaultAsync(p => p.Code.Equals("SPN-01"))).Id
                },
                new Temperature {
                    CelsiusDegree = 30,
                    DetectionPointId = (await dbContext.DetectionPoints.FirstOrDefaultAsync(p => p.Code.Equals("SPN-02"))).Id
                },
            };

            await dbContext.Temperatures.AddRangeAsync(temps);
            await dbContext.SaveChangesAsync();
        }
    }
}
