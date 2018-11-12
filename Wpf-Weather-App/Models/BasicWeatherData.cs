using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Weather_App.Models
{
    public class BasicWeatherData
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public double Longitude { get; set; }
        public int ZipCode { get; set; }
        public double Latitude { get; set; }
        public double Temperature { get; set; }
        public double WindSpeed { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        public BasicWeatherData()
        {

        }

        public BasicWeatherData(int id, double longitude, double latitude, int zipCode, double temperature, double windSpeed, string location, string icon, string description)
        {
            Id = id;
            Longitude = longitude;
            Latitude = latitude;
            ZipCode = zipCode;
            Temperature = temperature;
            WindSpeed = windSpeed;
            Location = location;
            Icon = icon;
            Description = description;
        }
    }
}
