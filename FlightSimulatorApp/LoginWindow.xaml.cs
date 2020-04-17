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
using System.ComponentModel;
using System.Text.RegularExpressions;


namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        bool buttonClicked;
        public LoginWindow()
        {
            InitializeComponent();
            buttonClicked = false;
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
                System.Windows.Media.Brush V = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFF2626");
                connection_error.Background = V;
                NotifyPropertyChanged("ButtonClicked");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int portAsString;
            string erorAsString = "";
            string ipAsString;
            try
            {
                if (port.Text == "")
                {
                    portAsString = 5402;
                    port.Text = "5402";
                }
                else
                {
                    erorAsString = "Enter Port number bitwin 1024-65536";
                    portAsString = Convert.ToInt32(port.Text);
                    if (portAsString < 1024 || portAsString > 65536)
                    {
                        connection_error.Content = "";
                        connection_error.Background = null;
                        throw new Exception();
                    }
                }
                if (ip.Text == "")
                {
                    ipAsString = "127.0.0.1";
                    ip.Text = "127.0.0.1";
                }
                else
                {
                    ipAsString = ip.Text;
                    Regex reg = new Regex("((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?).){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)");
                    if (!reg.IsMatch(ipAsString))
                    {
                        connection_error.Content = "";
                        connection_error.Background = null;
                        erorAsString = "Enter IP in his right format";
                        throw new Exception();
                    }
                }
                ButtonClicked = true;
                eror.Background = null;
                eror.Content = "";
            }
            catch
            {
                eror.Content = erorAsString;
                System.Windows.Media.Brush V = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFF2626");
                eror.Background = V;
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
