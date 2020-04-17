using FlightSimulatorApp.View;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        MainViewModel mainViewModel;
        bool buttonClicked;
        public MainWindow()
        {
            InitializeComponent();
            this.buttonClicked = false;
            this.mainViewModel = (Application.Current as App).MainVM;
            myControllers.DataContext = mainViewModel.getControllerVM();
            myMap.mapmap.DataContext = mainViewModel.getMapVM();
            myDash.DataContext = mainViewModel.getDashVM();
            knobX.DataContext = myControllers.myJoystick;
            knobY.DataContext = myControllers.myJoystick;
            myMap.exception.DataContext = (Application.Current as App).FlightGearModel;
        }
        private void Slider_ValueChanged(object sender,
            RoutedPropertyChangedEventArgs<double> e)
        {
            double x = myControllers.myJoystick.KnobPositionX;
            double y = myControllers.myJoystick.KnobPositionY;
            double r = myControllers.myJoystick.Base.Width / 6;
            double elev = x / r;
            double rudd = y / r;
            (Application.Current as App).FlightGearModel.movePlane(elev, rudd);
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public bool ButtonClicked
        {
            get { return this.buttonClicked; }
            set
            {
                this.buttonClicked = value;
                NotifyPropertyChanged("ButtonClicked");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current as App).FlightGearModel.disconnect();
            ButtonClicked = true;
        }
    }
}
