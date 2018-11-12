using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Weather_App.Models
{
    public class Context
    {
        public List<BasicWeatherData> TestBasicWeatherData { get; set; }
        public List<BasicWeatherData> BasicWeatherData { get; set; }
        public List<BasicWeatherData> FilteredBasicWeatherData { get; set; }

        public Context()
        {
            BasicWeatherData = new List<BasicWeatherData>();
            FilteredBasicWeatherData = new List<BasicWeatherData>();

            TestBasicWeatherData = new List<BasicWeatherData>()
            {
                new BasicWeatherData
                {
                    Id = 1001,
                    Longitude = 45,
                    Latitude = -86,
                    ZipCode = 49684,
                    Temperature = 43,
                    WindSpeed = 14,
                    Location = "Traverse City Area",
                    Description = "Another depressing cloudy day"
                },

                new BasicWeatherData
                {
                    Id = 1002,
                    Longitude = 34,
                    Latitude = 118,
                    ZipCode = 9005,
                    Temperature = 112,
                    WindSpeed = 8,
                    Location = "Los Angeles",
                    Description = "Too hot to be outside"
                }
            };
        }
    }
}
