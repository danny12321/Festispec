using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Utils
{
    class RouteDurationCalculator
    {
        public async Task<TimeSpan> CalculateRoute(string start, string end)
        {
            HttpClient client = new HttpClient();

            var jsonString = await client.GetStringAsync($"https://api.openrouteservice.org/v2/directions/driving-car?api_key=5b3ce3597851110001cf6248697761b8fe94467f88694a2d6522692e&start={start}&end={end}");
            JObject jObject = JObject.Parse(jsonString);
            int duduration = (int)jObject.SelectToken("features[0].properties.summary.duration");

            return new TimeSpan(0,0,duduration);
        }
    }
}
