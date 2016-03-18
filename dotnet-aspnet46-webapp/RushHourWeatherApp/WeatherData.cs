using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppForm
{
    public class sample
    {
        public string precipitation = string.Empty;
        public int temperatureF = -1;
        public string time = string.Empty;
        public string windDirection = string.Empty;
        public int windSpeedMph = -1;
   }

    public class WeatherData
    {
        public List<sample> samples = new List<sample>();
    }
}