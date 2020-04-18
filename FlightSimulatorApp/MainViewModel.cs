using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    //Class that managing all the view models of the app.
    public class MainViewModel
    {
        private DashBoardViewModel dashVM;
        private ControllersViewModel controllerVM;
        private MapViewModel mapVM;
        //Constructor that intizalizing all the view models with the given model. 
        public MainViewModel(IFlightGearModel flightGearModel)
        {
            this.dashVM = new DashBoardViewModel(flightGearModel);
            this.mapVM = new MapViewModel(flightGearModel);
            this.controllerVM = new ControllersViewModel(flightGearModel);
        }
        public DashBoardViewModel getDashVM()
        {
            return this.dashVM;
        }
        public ControllersViewModel getControllerVM()
        {
            return this.controllerVM;
        }
        public MapViewModel getMapVM()
        {
            return this.mapVM;
        }
    }
}
