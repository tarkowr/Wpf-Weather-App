using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
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
        List<Filters> _filters;
        Timer ResetTimer = new Timer();
        int refreshDisableTime = 15000;

        public MainWindow()
        {
            InitializeComponent();
            _bal = new BusinessLayer(_context);
            _filters = Enum.GetValues(typeof(Filters)).Cast<Filters>().ToList();
        }

        private void window_Main_Loaded(object sender, RoutedEventArgs e)
        {
            GetApiData();
            PopulateComboBox();
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            GetApiData();
            textBox_Output.Text = "Complete! Data loaded in from OpenWeatherMap.";
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                btn_Refresh.IsEnabled = true;
            });
        }

        private void btn_Details_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_Details.Background = Brushes.Black;
        }

        private void btn_Details_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_Details.Background = (Brush)bc.ConvertFrom("#FF193A70");
        }

        private void btn_Details_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid_Weather.SelectedItems.Count == 1)
            {
                BasicWeatherData basicWeatherData = (BasicWeatherData)dataGrid_Weather.SelectedItem;
                DetailsWindow detailsWindow = new DetailsWindow(basicWeatherData);
                detailsWindow.ShowDialog();
            }
        }

        private void btn_Help_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.ShowDialog();
        }

        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            _context.FilteredBasicWeatherData = _context.BasicWeatherData;
            BindDataToDataGrid(_context.FilteredBasicWeatherData);
            textBox_Output.Text = "";
            comboBox_Filter.SelectedIndex = -1;
            textBox_Filter.Text = "";
        }

        private void btn_EnterFilter_Click_1(object sender, RoutedEventArgs e)
        {
            string message;
            double value = 0, output = 0;
            int numberOfErrors = 0;

            if (_bal.ValidateInput(textBox_Filter.Text, out output, out message))
            {
                value = output;
            }
            else
            {
                numberOfErrors++;
                textBox_Output.Text = message;
            }

            if (comboBox_Filter.SelectedIndex == -1)
            {
                comboBox_Filter.SelectedIndex = 0;
            }

            if (numberOfErrors == 0)
            {
                switch (comboBox_Filter.Text.ToLower())
                {
                    case "not equal to":
                        _context.FilteredBasicWeatherData = _context.BasicWeatherData.Where(x => x.Temperature != value).ToList();
                        break;
                    case "greater than":
                        _context.FilteredBasicWeatherData = _context.BasicWeatherData.Where(x => x.Temperature > value).ToList();
                        break;
                    case "greater than or equal to":
                        _context.FilteredBasicWeatherData = _context.BasicWeatherData.Where(x => x.Temperature >= value).ToList();
                        break;
                    case "less than":
                        _context.FilteredBasicWeatherData = _context.BasicWeatherData.Where(x => x.Temperature < value).ToList();
                        break;
                    case "less than or equal to":
                        _context.FilteredBasicWeatherData = _context.BasicWeatherData.Where(x => x.Temperature <= value).ToList();
                        break;
                    default:
                        _context.FilteredBasicWeatherData = _context.BasicWeatherData.Where(x => x.Temperature == value).ToList();
                        break;
                }

                if (_context.FilteredBasicWeatherData.Count == 0)
                {
                    textBox_Output.Text = "No values returned from filter.";
                }
                else
                {
                    textBox_Output.Text = "";
                }

                BindDataToDataGrid(_context.FilteredBasicWeatherData);
            }
        }

        /// <summary>
        /// Get Api data using Business Access Layer and Bind data to DataGrid
        /// </summary>
        private void GetApiData()
        {
            try
            {
                _bal.ReadDataFromAPI();
                InitializeTimer(ResetTimer);
            }
            catch (Exception ex)
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

        /// <summary>
        /// Add relational operators to comboxbox
        /// </summary>
        private void PopulateComboBox()
        {
            foreach (Filters filter in _filters)
            {
                if (filter != Filters.None)
                {
                    comboBox_Filter.Items.Add(Regex.Replace(filter.ToString(), "([a-z])([A-Z])", "$1 $2"));
                }
            }
        }

        /// <summary>
        /// Start Timer and disable refresh button
        /// </summary>
        /// <param name="_timer"></param>
        private void InitializeTimer(Timer _timer)
        {
            _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _timer.Interval = refreshDisableTime;
            _timer.AutoReset = false;
            _timer.Start();

            btn_Refresh.IsEnabled = false;
        }
    }
}
