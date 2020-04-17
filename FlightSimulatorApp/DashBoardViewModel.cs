using System;
using System.ComponentModel;

namespace FlightSimulatorApp
{
    public class DashBoardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IFlightGearModel model;
        public DashBoardViewModel(IFlightGearModel model)
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

        public double VM_Heading_deg
        {
            get { return model.Heading_deg; }

        }
        public double VM_Vertical_speed
        {
            get
            {
                return model.Vertical_speed;
            }
        }
        public double VM_Ground_speed
        {
            get { return model.Ground_speed; }
        }
        public double VM_Air_speed
        {
            get { return model.Air_speed; }
        }
        public double VM_Gps_Indicated_altitude
        {
            get { return model.Gps_Indicated_altitude; }
        }
        public double VM_Internal_roll
        {
            get { return model.Internal_roll; }
        }
        public double VM_Internal_pitch
        {
            get { return model.Internal_pitch; }
        }
        public double VM_Altimeter_Indicated_altitude
        {
            get { return model.Altimeter_Indicated_altitude; }
        }

    }
}
