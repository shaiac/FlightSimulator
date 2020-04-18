using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    /**
        The view model of all the Controllers of the App, 2 sliders for the Throttle and Aileron
        and for the Joystick values rudder and elevator. We will bind all those properties to the
        match Controller in the view.
     */
    public class ControllersViewModel
    {
        private IFlightGearModel model;
        double rudder;
        double elevator;
        //Constructor, setting the app model
        public ControllersViewModel(IFlightGearModel model)
        {
            this.model = model;
        }
        
        public double VM_Throttle
        {
            get { return model.Throttle; }
            set
            {
                model.Throttle = value;
            }
        }
        public double VM_Aileron
        {
            get { return model.Aileron; }
            set
            {
                model.Aileron = value;
            }
        }

        public double VM_Rudder
        {
            get { return rudder; }
            set
            {
                rudder = value;
                model.movePlane(elevator, rudder);
            }
        }
        public double VM_Elevator
        {
            get { return elevator; }
            set
            {
                elevator = value;
                model.movePlane(elevator, rudder);
            }
        }
    }
}
