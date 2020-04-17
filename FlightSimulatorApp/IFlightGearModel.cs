using Microsoft.Maps.MapControl.WPF;
using System.ComponentModel;

namespace FlightSimulatorApp
{
    public interface IFlightGearModel : INotifyPropertyChanged
    {
        void connect(string ip, int port);
        void disconnect();
        void start();

        //Properties
        double Heading_deg { set; get; }
        double Vertical_speed { set; get; }
        double Ground_speed { set; get; }
        double Air_speed { set; get; }
        double Gps_Indicated_altitude { set; get; }
        double Internal_roll { set; get; }
        double Internal_pitch { set; get; }
        double Altimeter_Indicated_altitude { set; get; }
        double Latitude { set; get; }
        double Longitude { set; get; }
        double Throttle { set; get; }
        double Aileron { set; get; }
        Location Location { get; }
        //active
        void movePlane(double elevator, double rudder);
    }
}
