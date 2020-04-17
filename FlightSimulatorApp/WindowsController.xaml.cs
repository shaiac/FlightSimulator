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
using System.Windows.Shapes;
using System.Threading;
using System.ComponentModel;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for WindowsController.xaml
    /// </summary>
    public partial class WindowsController : Window, INotifyPropertyChanged
    {
        App app;
        public event PropertyChangedEventHandler PropertyChanged;
        string error = "";
        public WindowsController()
        {
            app = (Application.Current as App);
            InitializeComponent();
            LoginButton.DataContext = app.LoginWindow;
            LogoutButton.DataContext = app.MainWindowProp;
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {

                app.FlightGearModel.connect(app.LoginWindow.ip.Text, int.Parse(app.LoginWindow.port.Text));
                app.LoginWindow.Hide();
                app.MainWindowProp.Show();
                LoginButton.IsChecked = false;
            }
            catch
            {
                Error = "Un able to connect the server with this ip and port...";
                NotifyPropertyChanged("Error");
                LoginButton.IsChecked = false;
            }
        }

        private void ExitButton_checked(object sender, RoutedEventArgs e)
        {
            app.Application_New();
        }

        public string Error
        {
            get { return error; }
            set { error = value; }
        }
    }
}
