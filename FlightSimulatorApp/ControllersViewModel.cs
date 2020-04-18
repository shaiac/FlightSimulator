using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
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
        /**
         Property of the throttle
         */
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
