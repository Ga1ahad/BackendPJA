using Clothesy.WeatherApiService.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Clothesy.WeatherApiService
{
    public class WeatherService
    {
        public Root weather = new Root();
        public async Task<List<TripPeriod>> LoadWeatherInformation(string city, string startTime, string endTime)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://aerisweather1.p.rapidapi.com/forecasts/" + city + ",pl?from=" + startTime + "&to=" + endTime),
                Headers =
                {
                    { "x-rapidapi-key", "f0347121e3mshfc91a25705ad38bp1f085bjsnf795e52eb9cb" },
                    { "x-rapidapi-host", "aerisweather1.p.rapidapi.com" },
                },
            };

            using (var response = await ApiHelper.ApiClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var bodyText = await response.Content.ReadAsStringAsync();
                JObject body = JObject.Parse(bodyText);
                
                weather = JsonConvert.DeserializeObject<Root>(body.ToString());

                List<TripPeriod> periods = new List<TripPeriod>();
                foreach (var item in weather.response[0].periods)
                {
                    periods.Add(new TripPeriod
                    {
                        avgTempC = item.avgTempC,
                        Date = item.validTime,
                        weather = item.weather,
                        windSpeedKPH = item.windSpeedKPH
                    });
                }
                
                return periods;

            }
        }
    }
}
