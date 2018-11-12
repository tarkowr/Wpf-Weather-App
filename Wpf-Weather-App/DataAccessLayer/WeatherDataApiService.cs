using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wpf_Weather_App.Models;

namespace Wpf_Weather_App.DataAccessLayer
{
    public class WeatherDataApiService : IApiService
    {
        /// <summary>
        /// Build the API call and return an instance of WeatherData
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public WeatherData GetWeatherData(int zipCode)
        {
            string url;
            string key = AppConfig.apiKey;

            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append("http://api.openweathermap.org/data/2.5/weather?");
            sb.Append($"&zip={zipCode}");
            sb.Append($"&appid={key}");

            url = sb.ToString();

            WeatherData currentWeather = new WeatherData();

            WeatherData getCurrentWeather = HttpGetCurrentWeatherByLocation(url);

            currentWeather = getCurrentWeather;

            return currentWeather;
        }

        /// <summary>
        /// Use WebClient to make an API call and Convert the JSON file into a WeatherData object
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        static WeatherData HttpGetCurrentWeatherByLocation(string url)
        {
            string result = null;

            using (WebClient syncClient = new WebClient())
            {
                result = syncClient.DownloadString(url);
            }

            WeatherData currentWeather = JsonConvert.DeserializeObject<WeatherData>(result);

            return currentWeather;
        }
    }
}
