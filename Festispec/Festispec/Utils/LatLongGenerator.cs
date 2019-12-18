using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Utils
{
    public class LatLongGenerator
    {
        public async Task<string> GenerateLatLong(string country, string city, string street, string housenumber)
        {
            HttpClient client = new HttpClient();

            var jsonString = await client.GetStringAsync($"https://api.opencagedata.com/geocode/v1/json?q={country} {city} {street} {housenumber}&key=c72b85fa977e4baea1d03ecfba0ff18e&language=nl&pretty=1");
            JObject jObject = JObject.Parse(jsonString);
            string lan = (string)jObject.SelectToken("results[0].geometry.lat");
            string lng = (string)jObject.SelectToken("results[0].geometry.lng");

            return lan + "," + lng;
        }
    }
}
