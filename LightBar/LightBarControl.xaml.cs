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
    /// Interaction logic for LightBar.xaml
    /// </summary>
    public partial class LightBarControl : UserControl
    {
        internal double maxLevel = double.MinValue;

        public double TheValue
        {
            get { return (int)GetValue(TheValueProperty); }
            set { SetValue(TheValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TheValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TheValueProperty =
            DependencyProperty.Register("TheValue", typeof(double), typeof(LightBarControl), new PropertyMetadata(0.0, OnTheValueChanged));

        private static void OnTheValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as LightBarControl;
            foreach (var child in control.ledStack.Children)
            {
                if (child is LED)
                {
                    double newValue = (double)e.NewValue;
                    ((LED)child).UpdateFill(newValue);

                    if (newValue > control.maxLevel)
                    {
                        control.maxLevel = newValue;
                        control.pbMaxLevel.Value = newValue;
                    }
                }
            }
        }

        public LightBarControl()
        {
            InitializeComponent();
            DataContext = this;

            Loaded += (s, e) =>
            {
                var ledOVR = new LED("OVR");
                ledOVR.Margin = new Thickness(5.0, 0.0, 5.0, 2.0);
                ledStack.Children.Add(ledOVR);

                // I typed these in backwards, and was too lazy to reverse, so call .Reverse function
                double[] levels = new double[] { -72, -56, -48, -42, -36, -30, -24, -18, -14, -12, -10, -8, -6, -4, -2, 0 };
                foreach (var level in levels.Reverse())
                {
                    var led = new LED(level.ToString());
                    led.Margin = new Thickness(5.0, 0.0, 5.0, 2.0);
                    ledStack.Children.Add(led);
                }
            };
        }
    }
}
