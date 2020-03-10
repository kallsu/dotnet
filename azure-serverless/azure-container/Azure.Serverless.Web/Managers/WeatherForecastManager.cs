using Azure.Serverless.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Azure.Serverless.Web.Managers
{
    public class WeatherForecastManager
    {
        private static readonly Random GetRandom = new Random();

        public WeatherForecastManager()
        {
        }

        public async Task<IList<WeatherForecast>> GetForecasts()
        {
            var totalListSize = GetRandom.Next(10, 40);
            var now = DateTime.UtcNow;

            var forecasts = new List<WeatherForecast>(0);

            for (var i = 0; i < totalListSize; i++)
            {

                var min = GetRandom.Next(-10, 18);
                var max = GetRandom.Next(19, 40);

                forecasts.Add(
                new WeatherForecast()
                {
                    Date = now.AddHours(i),
                    TemperatureC = GetRandom.Next(min, max),
                    Summary = $"Weather between [{min}, {max}]"
                });
            }

            return forecasts;
        }
    }
}
