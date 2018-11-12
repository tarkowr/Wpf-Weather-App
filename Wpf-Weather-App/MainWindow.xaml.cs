using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf_Weather_App.BusinessAccessLayer;
using Wpf_Weather_App.Models;

namespace Wpf_Weather_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BusinessLayer _bal;
        Context _context = new Context();
        BrushConverter bc = new BrushConverter();

        public MainWindow()
        {
            InitializeComponent();
            _bal = new BusinessLayer(_context);
        }

        private void window_Main_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _bal.ReadDataFromAPI();
            }
            catch(Exception ex)
            {
                textBox_Output.Text = ex.Message;
            }
            finally
            {
                BindDataToDataGrid(_context.BasicWeatherData);
            }
        }

        /// <summary>
        /// Bind current data to winforms grid control
        /// </summary>
        /// <param name="basicWeatherData"></param>
        private void BindDataToDataGrid(List<BasicWeatherData> basicWeatherData)
        {
            dataGrid_Weather.ItemsSource = basicWeatherData;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Details_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_Details.Background = Brushes.Black;
        }

        private void btn_Details_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_Details.Background = (Brush)bc.ConvertFrom("#FF193A70");
        }

        private void btn_Help_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.ShowDialog();
        }

        private void btn_Details_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
