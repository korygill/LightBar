using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SynchronizationContext synchronizationContext;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            synchronizationContext = SynchronizationContext.Current;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(
                () =>
                {
                    var sw = new Stopwatch();
                    sw.Start();
                    // 20 per second, 200 is 10 seconds worth
                    int numIterations = 200;
                    int msDelay = 50;
                    for (var count = 0; count < numIterations; count++)
                    {
                        var r = new Random().Next(-720, 10);
                        double d = ((double)r) / 10.0;

                        synchronizationContext.Send(new SendOrPostCallback(o =>
                        {
                            lightBar.TheValue = (double)o;

                            // this could move, but simulate delay reset here
                            if ((count % 20) == 0)
                            {
                                lightBar.pbMaxLevel.Value = lightBar.pbMaxLevel.Minimum;
                                lightBar.maxLevel = double.MinValue;
                            }
                        }), d);

                        // ensure no more than 20 per second
                        System.Threading.Thread.Sleep(msDelay);
                    }
                    sw.Stop();
                    synchronizationContext.Send(new SendOrPostCallback(o =>
                    {
                        elapsed.Text = (string)o;
                        iterations.Text = numIterations.ToString();
                        delay.Text = msDelay.ToString();
                    }), sw.Elapsed.ToString());
                });
        }
    }
}
