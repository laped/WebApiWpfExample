using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WebApiWpfExample.ApiClient;

namespace WebApiWpfExample.WpfApp
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<WeatherObservationDto> weatherData;

        public ICommand LoadData { get; set; }
        public ObservableCollection<WeatherObservationDto> WeatherData
        {
            get => weatherData;
            set
            {
                if (weatherData != value)
                {
                    weatherData = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(WeatherData)));
                }
            }
        }

        public MainWindowViewModel()
        {
            LoadData = new RelayCommand(async () => await OnLoadData());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async Task OnLoadData()
        {
            var client = new WeatherClient("https://localhost:44395", new HttpClient());
            var data = await client.WeatherForecastAllAsync();

            WeatherData = new ObservableCollection<WeatherObservationDto>(data);
        }
    }
}
