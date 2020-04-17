using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainViewModel mainVM;
        private IClient myClient;
        private IFlightGearModel flightGearModel;
        MainWindow mainWindow;
        LoginWindow loginWindow;
        WindowsController windowsController;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.myClient = new MyClient();
            this.flightGearModel = new FlightGearModel(myClient);
            mainVM = new MainViewModel(flightGearModel);
            mainWindow = new MainWindow();
            loginWindow = new LoginWindow();
            windowsController = new WindowsController();
            loginWindow.Show();
            loginWindow.connection_error.DataContext = windowsController;
        }


        public MainViewModel MainVM
        {
            get { return this.mainVM; }
        }
        public IFlightGearModel FlightGearModel
        {
            get { return this.flightGearModel; }
        }
        public LoginWindow LoginWindow
        {
            get { return this.loginWindow; }
            set
            {
                this.loginWindow = value;
            }
        }
        public IClient MyClient
        {
            get { return this.myClient; }
            set
            {
                this.myClient = value;
            }
        }
        public MainWindow MainWindowProp
        {
            get { return this.mainWindow; }
            set { this.mainWindow = value; }
        }
        public void Application_New()
        {
            this.myClient = new MyClient();
            this.flightGearModel = new FlightGearModel(myClient);
            mainVM = new MainViewModel(flightGearModel);
            mainWindow.Close();
            mainWindow = new MainWindow();
            loginWindow.Close();
            loginWindow = new LoginWindow();
            windowsController = new WindowsController();
            loginWindow.Show();
        }
    }
}
