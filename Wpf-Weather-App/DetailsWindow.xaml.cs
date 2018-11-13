using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpf_Weather_App.Models;

namespace Wpf_Weather_App
{
    /// <summary>
    /// Interaction logic for DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window
    {
        BasicWeatherData _basicWeatherData;

        public DetailsWindow(BasicWeatherData basicWeatherData)
        {
            InitializeComponent();
            _basicWeatherData = basicWeatherData;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            window_Details.Title = $"Weather for {_basicWeatherData.Location} ({_basicWeatherData.ZipCode})";
            lbl_Title.Content = _basicWeatherData.Location;
            image_Weather.Source = new BitmapImage(new Uri("http://openweathermap.org/img/w/" + _basicWeatherData.Icon + ".png"));
            lbl_ZipValue.Content = _basicWeatherData.ZipCode.ToString();
            lbl_LatValue.Content = _basicWeatherData.Latitude.ToString();
            lbl_LonValue.Content = _basicWeatherData.Longitude.ToString();
            lbl_TempValue.Content = _basicWeatherData.Temperature.ToString();
            lbl_WindValue.Content = _basicWeatherData.WindSpeed.ToString();
            lbl_DescValue.Content = _basicWeatherData.Description.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
