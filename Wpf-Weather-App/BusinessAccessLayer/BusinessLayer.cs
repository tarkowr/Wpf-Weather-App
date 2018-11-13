using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf_Weather_App.Models;
using Wpf_Weather_App.DataAccessLayer;

namespace Wpf_Weather_App.BusinessAccessLayer
{
    public class BusinessLayer
    {
        List<int> zipCodes = new List<int> { 49684, 90005, 48127, 80014, 33101 };
        Context _context;

        public BusinessLayer(Context context)
        {
            _context = context;
        }

        public void ReadDataFromAPI()
        {
            IApiService apiService = new WeatherDataApiService();

            _context.BasicWeatherData.Clear();

            foreach (int zip in zipCodes)
            {
                WeatherData tempWeatherData = apiService.GetWeatherData(zip);
                _context.BasicWeatherData.Add(new BasicWeatherData(HandleId(), 
                    Math.Round(tempWeatherData.Coord.Lon, 1),
                    Math.Round(tempWeatherData.Coord.Lat, 1), 
                    zip, 
                    Math.Round(ConvertToFahrenheit(tempWeatherData.Main.Temp), 1),
                    Math.Round(tempWeatherData.Wind.Speed, 1), 
                    tempWeatherData.Name, 
                    tempWeatherData.Weather.First().Icon,
                    tempWeatherData.Weather.First().Description));
            }

            _context.BasicWeatherData = _context.BasicWeatherData.OrderBy(x => x.Location).ToList();
        }

        /// <summary>
        /// Validate Filter Value Input
        /// </summary>
        /// <param name="value"></param>
        /// <param name="output"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool ValidateInput(string value, out double output, out string message)
        {
            message = "";

            if (double.TryParse(value, out output))
            {
                if (output >= 200 || output <= -200)
                {
                    message = "The Filter Value textbox must have a number between -200 and 200.";
                    return false;
                }
                else
                {
                    return true; ;
                }
            }
            else
            {
                message = "The Filter Value textbox must be a number.";
                return false;
            }
        }

        /// <summary>
        /// Convert Kalvin to Fahrenheit
        /// </summary>
        /// <param name="degreesKalvin"></param>
        /// <returns></returns>
        static double ConvertToFahrenheit(double degreesKalvin)
        {
            return (degreesKalvin - 273.15) * 1.8 + 32;
        }

        /// <summary>
        /// Generate a unique Id with a base of 10001
        /// </summary>
        /// <returns></returns>
        private int HandleId()
        {
            return 10001 + _context.BasicWeatherData.Count;
        }
    }
}
