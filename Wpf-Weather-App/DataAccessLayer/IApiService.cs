using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf_Weather_App.Models;

namespace Wpf_Weather_App.DataAccessLayer
{
    public interface IApiService
    {
        WeatherData GetWeatherData(int zipCode);
    }
}
