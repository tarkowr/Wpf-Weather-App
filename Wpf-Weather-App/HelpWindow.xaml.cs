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

namespace Wpf_Weather_App
{
    /// <summary>
    /// Interaction logic for HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();
        }

        private void window_Help_Loaded(object sender, RoutedEventArgs e)
        {
            this.lbl_HelpText.Content = "Clicking on the Details button when a row is selected will display additional information. \n" +
                "The Get Latest Data button uses OpenWeatherMap's API to retrieve updated weather data. \n" +
                "Use the controls above the weather data to filter the weather data by temperature.";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
