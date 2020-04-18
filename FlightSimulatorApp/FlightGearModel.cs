using Microsoft.Maps.MapControl.WPF;
using System;
using System.ComponentModel;
using System.Threading;

namespace FlightSimulatorApp
{
    public class FlightGearModel : IFlightGearModel
    {
        IClient client;
        volatile bool stop;
        public event PropertyChangedEventHandler PropertyChanged;
        private double heading_deg;
        private double vertical_speed;
        private double ground_speed;
        private double air_speed;
        private double gps_Indicated_altitude;
        private double internal_roll;
        private double internal_pitch;
        private double altimeter_Indicated_altitude;
        private double latitude;
        private double longitude;
        private double elevator;
        private double rudder;
        private Location location;
        private double throttle;
        private bool throttleIsChanged;
        private double aileron;
        private bool aileronIsChanged;
        private string exceptionType;

        bool planeMove;
        public FlightGearModel(IClient client)
        {
            this.client = client;
            this.stop = false;
            this.planeMove = false;
        }

        public double Throttle
        {
            get { return throttle; }
            set
            {
                if (throttle != value)
                {
                    throttle = value;
                    throttleIsChanged = true;
                }
            }
        }
        public double Aileron
        {
            get { return aileron; }
            set
            {
                if (aileron != value)
                {
                    aileron = value;
                    aileronIsChanged = true;
                }
            }
        }
        public double Heading_deg
        {
            get { return heading_deg; }
            set
            {
                heading_deg = value;
                NotifyPropertyChanged("Heading_deg");
            }
        }
        public double Vertical_speed
        {
            get { return vertical_speed; }
            set
            {
                vertical_speed = value;
                NotifyPropertyChanged("Vertical_speed");
            }
        }
        public double Ground_speed
        {
            get { return ground_speed; }
            set
            {
                ground_speed = value;
                NotifyPropertyChanged("Ground_speed");
            }
        }
        public double Air_speed
        {
            get { return air_speed; }
            set
            {
                air_speed = value;
                NotifyPropertyChanged("Air_speed");
            }
        }
        public double Gps_Indicated_altitude
        {
            get { return gps_Indicated_altitude; }
            set
            {
                gps_Indicated_altitude = value;
                NotifyPropertyChanged("Gps_Indicated_altitude");
            }
        }
        public double Internal_roll
        {
            get { return internal_roll; }
            set
            {
                internal_roll = value;
                NotifyPropertyChanged("Internal_roll");
            }
        }
        public double Internal_pitch
        {
            get { return internal_pitch; }
            set
            {
                internal_pitch = value;
                NotifyPropertyChanged("Internal_pitch");
            }
        }
        public double Altimeter_Indicated_altitude
        {
            get { return altimeter_Indicated_altitude; }
            set
            {
                altimeter_Indicated_altitude = value;
                NotifyPropertyChanged("Altimeter_Indicated_altitude");
            }
        }

        public double Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                Location loc = new Location(Latitude, Longitude);
                this.Location = loc;
            }
        }

        public double Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                Location loc = new Location(Latitude, Longitude);
                this.Location = loc;
            }
        }

        public Location Location
        {
            get { return this.location; }
            set
            {
                this.location = value;
                NotifyPropertyChanged("Location");
            }
        }

        public string ExceptionType
        {
            get { return this.exceptionType; }
            set
            {
                this.exceptionType = value;
                NotifyPropertyChanged("ExceptionType");
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        //Connecting to the server
        public void connect(string ip, int port)
        {
            this.client.connect(ip, port);
            this.start();

        }
        /**
         Running a loop to get or set properties values from the server.
         each time we are writing a request and reading the answer of the server, checking the answer that
         we got if in range, not error..
         */
        public void start()
        {
            string buffer;
            double tempDouble;
            new Thread(delegate () {
                while (!stop)
                {
                    //Try and catch if we got exception while reading/writing we dont want the app to stuck. 
                    try
                    {
                        client.write("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                        buffer = client.read();
                        if (!buffer.Contains("ERR"))
                        {
                            tempDouble = Double.Parse(buffer);
                            Heading_deg = System.Math.Round(checkIfInRange("Heading_deg", tempDouble), 3);
                        }
                        else
                            ExceptionType = "Error Reading Heading deg";

                        client.write("get /instrumentation/gps/indicated-vertical-speed\n");
                        buffer = client.read();
                        if (!buffer.Contains("ERR"))
                        {
                            tempDouble = Double.Parse(buffer);
                            Vertical_speed = System.Math.Round(checkIfInRange("Vertical_speed", tempDouble), 3);
                        }
                        else
                            ExceptionType = "ErErroror Reading Vertical speed";

                        client.write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                        buffer = client.read();
                        if (!buffer.Contains("ERR"))
                        {
                            tempDouble = Double.Parse(buffer);
                            Ground_speed = System.Math.Round(checkIfInRange("Ground_speed", tempDouble), 3);
                        }
                        else
                            ExceptionType = "Error Reading Ground speed";

                        client.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                        buffer = client.read();
                        if (!buffer.Contains("ERR"))
                        {
                            tempDouble = Double.Parse(buffer);
                            Air_speed = System.Math.Round(checkIfInRange("Air_speed", tempDouble), 3);
                        }
                        else
                            ExceptionType = "Error Reading Air speed";

                        client.write("get /instrumentation/gps/indicated-altitude-ft\n");
                        buffer = client.read();
                        if (!buffer.Contains("ERR"))
                        {
                            tempDouble = Double.Parse(buffer);
                            Gps_Indicated_altitude = System.Math.Round(checkIfInRange("Gps_Indicated_altitude", tempDouble), 3);
                        }
                        else
                            ExceptionType = "Error Reading Gps Indicated altitude";

                        client.write("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                        buffer = client.read();
                        if (!buffer.Contains("ERR"))
                        {
                            tempDouble = Double.Parse(buffer);
                            Internal_roll = System.Math.Round(checkIfInRange("Internal_roll", tempDouble), 3);
                        }
                        else
                            ExceptionType = "Error Reading Internal roll";

                        client.write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                        buffer = client.read();
                        if (!buffer.Contains("ERR"))
                        {
                            tempDouble = Double.Parse(buffer);
                            Internal_pitch = System.Math.Round(checkIfInRange("Internal_pitch", tempDouble), 3);
                        }
                        else
                            ExceptionType = "Error Reading Internal pitch";

                        client.write("get /instrumentation/altimeter/indicated-altitude-ft\n");
                        buffer = client.read();
                        if (!buffer.Contains("ERR"))
                        {
                            tempDouble = Double.Parse(buffer);
                            Altimeter_Indicated_altitude = System.Math.Round(checkIfInRange("Altimeter_Indicated_altitude", tempDouble), 3);
                        }
                        else
                            ExceptionType = "Error Reading Altimeter Indicated altitude";

                        client.write("get /position/latitude-deg\n");
                        buffer = client.read();
                        if (!buffer.Contains("ERR"))
                        {
                            tempDouble = Double.Parse(buffer);
                            //Checking if the latitude is in the earth range.
                            if (tempDouble >= -90 && tempDouble <= 90)
                            {
                                Latitude = tempDouble;
                            }
                            else
                            {
                                ExceptionType = "The Latitude is not in range";
                            }
                        }
                        else
                            ExceptionType = "Error Reading the Latitude position";

                        client.write("get /position/longitude-deg\n");
                        buffer = client.read();
                        if (!buffer.Contains("ERR"))
                        {
                            tempDouble = Double.Parse(buffer);
                            //Checking if the longitude is in the earth range.
                            if (tempDouble >= -180 && tempDouble <= 180)
                            {
                                Longitude = tempDouble;
                            }
                            else
                            {
                                ExceptionType = "The Longitude is not in range";
                            }

                        }
                        else
                            ExceptionType = "Error Reading the Longitude position";

                        //All the Setting commands.
                        if (aileronIsChanged)
                        {
                            client.write("set /controls/flight/aileron " + aileron + "\n");
                            buffer = client.read();
                            aileronIsChanged = false;
                        }
                        if (throttleIsChanged)
                        {
                            client.write("set /controls/engines/current-engine/throttle " + throttle + "\n");
                            client.read();
                            throttleIsChanged = false;
                        }
                        if (planeMove)
                        {
                            client.write("set /controls/flight/rudder " + rudder + "\n");
                            client.read();
                            client.write("set /controls/flight/elevator " + elevator + "\n");
                            client.read();
                            planeMove = false;
                        }

                        Thread.Sleep(250);
                    }
                    catch
                    {
                        ExceptionType = "Error, The Server is Down";
                    }
                }
            }).Start();
        }
        /**
          Method that checks by the property name if the value that we got is in the range if not 
          returning the closest value that in range
         */
        public double checkIfInRange(string propName, double value)
        {
            if (propName == "Heading_deg")
            {
                if (value < 5)
                    return 5.0;
                if (value > 7)
                    return 7.0;
            }
            else if (propName == "Vertical_speed")
            {
                if (value < 7)
                    return 7.0;
                if (value > 9)
                    return 9.0;
            }
            else if (propName == "Ground_speed")
            {
                if (value < 6)
                    return 6.0;
                if (value > 8)
                    return 8.0;
            }
            else if (propName == "Air_speed")
            {
                if (value < 0)
                    return 0.0;
                if (value > 2)
                    return 2.0;
            }
            else if (propName == "Gps_Indicated_altitude")
            {
                if (value < 1)
                    return 1.0;
                if (value > 3)
                    return 3.0;
            }
            else if (propName == "Internal_roll")
            {
                if (value < 2)
                    return 2.0;
                if (value > 4)
                    return 4.0;
            }
            else if (propName == "Internal_pitch")
            {
                if (value < 3)
                    return 3.0;
                if (value > 5)
                    return 5.0;
            }
            else if (propName == "Altimeter_Indicated_altitude")
            {
                if (value < 4)
                    return 4.0;
                if (value > 6)
                    return 6.0;
            }
            return value;
        }
        //when activated, the next round of the loop in the start method the rudder and elevator will set 
        public void movePlane(double elevator, double rudder)
        {
            planeMove = true;
            this.elevator = elevator;
            this.rudder = rudder;
        }
        //Disconnecting from the server.
        public void disconnect()
        {
            this.stop = true;
            this.client.disconnect();
        }
    }
}
