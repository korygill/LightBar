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

namespace LightBar
{
    /// <summary>
    /// Interaction logic for LED.xaml
    /// </summary>
    public partial class LED : UserControl
    {
        private SolidColorBrush myFill;

        public string MyLabel
        {
            get { return (string)GetValue(MyLabelProperty); }
            set { SetValue(MyLabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyLabel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyLabelProperty =
            DependencyProperty.Register("MyLabel", typeof(string), typeof(LED), new PropertyMetadata(string.Empty, OnMyLabelChanged));

        private static void OnMyLabelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as LED;
            var val = e.NewValue as string;
            int ival;
            if (int.TryParse(val, out ival))
            {
                if (ival <= -24)
                {
                    control.myFill = Brushes.Green;
                }
                else if (ival <= -10)
                {
                    control.myFill = Brushes.Yellow;
                }
                else if (ival <= 0)
                {
                    control.myFill = Brushes.Red;
                }
                else
                {
                    // if you change OVR to 1 for example.
                    control.myFill = Brushes.Purple;
                }
            }
            else
            {
                control.MyLabel = val;
                control.myFill = Brushes.Purple;
            }

            control.ledEllipse.Fill = control.myFill;
        }

        public LED()
        {
            InitializeComponent();
            DataContext = this;
        }

        public LED(string level) : this()
        {
            MyLabel = level;
        }

        public void UpdateFill(double ival)
        {
            double label;
            if (double.TryParse(MyLabel, out label))
            {
                if (ival >= label)
                {
                    ledEllipse.Fill = myFill;
                }
                else
                {
                    ledEllipse.Fill = Brushes.Transparent;
                }
            }
            else
            {
                if (ival >= 0.0)
                {
                    ledEllipse.Fill = myFill;
                }
                else
                {
                    ledEllipse.Fill = Brushes.Transparent;
                }
            }
        }
    }
}
