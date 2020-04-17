using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulatorApp.View
{
    /// <summary>
    /// Interaction logic for JoyStick.xaml
    /// </summary>
    public partial class JoyStick : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private double knobPositionX;
        private double knobPositionY;
        private Storyboard story;
        public JoyStick()
        {
            InitializeComponent();
            knobPositionX = 0;
            knobPositionY = 0;
            this.story = (Storyboard)Knob.FindResource("CenterKnob");
            story.Completed += new EventHandler(CompleteStory);

        }
        private void CompleteStory(object sender, EventArgs e)
        {
            story.Stop(Knob);
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        public double KnobPositionX
        {
            get { return this.knobPositionX; }
            set
            {
                this.knobPositionX = value;
                NotifyPropertyChanged("KnobPositionX");
            }
        }
        public double KnobPositionY
        {
            get { return this.knobPositionY; }
            set
            {
                this.knobPositionY = value;
                NotifyPropertyChanged("KnobPositionY");
            }
        }

        private Point mouseDownPoint;
        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                mouseDownPoint = e.GetPosition(this);
            }
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                double x = e.GetPosition(this).X - mouseDownPoint.X;
                double y = e.GetPosition(this).Y - mouseDownPoint.Y;
                if (Math.Sqrt(x * x + y * y) < Base.Width / 6)
                {
                    Mouse.Capture(this.KnobBase);
                    knobPosition.X = x;
                    knobPosition.Y = y;
                    this.KnobPositionX = x;
                    this.KnobPositionY = y;
                }
                else
                {
                    double cut = Math.Sqrt(x * x + y * y) - Base.Width / 6;
                    double dec = 1 - cut / (Math.Sqrt(x * x + y * y));
                    x = x * dec;
                    y = y * dec;
                    knobPosition.X = x;
                    knobPosition.Y = y;
                    this.KnobPositionX = x;
                    this.KnobPositionY = y;
                }
            }
        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            story.Begin(Knob, true);
            knobPosition.X = 0;
            knobPosition.Y = 0;
            Mouse.Capture(null);
        }
    }
}
