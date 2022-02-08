using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Weather_Gadget.Model;

namespace Weather_Gadget.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {

        private List<WeatherModel> forecast = new List<WeatherModel>();

        public List<WeatherModel> Forecast
        {
            get { return forecast; }
            set
            {
                forecast = value;
                OnPropertyChanged("Forecast");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
