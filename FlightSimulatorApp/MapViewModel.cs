using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    /**
        The view model of the Map, There are two properties that sets the  
        map location Latitude and Longitude. 
     */
    public class MapViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IFlightGearModel model;
        public MapViewModel(IFlightGearModel model)
        {
            this.model = model;
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public double VM_Latitude
        {
            get { return this.model.Latitude; }
            set { model.Latitude = value; }
        }

        public double VM_Longitude
        {
            get { return model.Longitude; }
            set { model.Longitude = value; }
        }

        public Location VM_Location
        {
            get { return model.Location; }
        }
    }
}
