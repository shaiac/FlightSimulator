using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    public class MainViewModel
    {
        private DashBoardViewModel dashVM;
        private ControllersViewModel controllerVM;
        private MapViewModel mapVM;
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
