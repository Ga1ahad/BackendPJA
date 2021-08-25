using System;
using System.Collections.Generic;
using System.Text;

namespace Clothesy.WeatherApiService.Models
{
    public class TripPeriod
    {
        public DateTime Date { get; set; }
        public int avgTempC { get; set; }
        public int windSpeedKPH { get; set; }
        public string weather { get; set; }
    }
}
