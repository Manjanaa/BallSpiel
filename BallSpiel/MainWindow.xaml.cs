using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace BallSpiel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer _animationsTimer = new DispatcherTimer();
        private bool ellBallDriftingRight = true;
        private bool ellBallDriftingDown = true;
        private int zaehler = 0;

        public MainWindow()
        {
            InitializeComponent();

            _animationsTimer.Interval = TimeSpan.FromMilliseconds(50);
            _animationsTimer.Tick += positioningellBall;
        }

        private void positioningellBall(object sender, EventArgs e)
        {
            var x = Canvas.GetLeft(ellBall);
            var y = Canvas.GetTop(ellBall);

            // Horizontal Movement
            if (ellBallDriftingRight)
            {
                Canvas.SetLeft(ellBall, x + 5);
            }
            else
            {
                Canvas.SetLeft(ellBall, x - 5);
            }

            if (x >= canMatchfield.ActualWidth - ellBall.ActualWidth)
            {
                ellBallDriftingRight = false;
            }
            else if (x <= 0)
            {
                ellBallDriftingRight = true;
            }

            // Vertical Movement
            if (ellBallDriftingDown)
            {
                Canvas.SetTop(ellBall, y + 5);
            }
            else
            {
                Canvas.SetTop(ellBall, y - 5);
            }
            if (y >= canMatchfield.ActualHeight - ellBall.ActualHeight)
            {
                ellBallDriftingDown = false;
            }
            else if (y <= 0)
            {
                ellBallDriftingDown = true;
            }
        }

        private void btnStartStop_Click(object sender, RoutedEventArgs e)
        {
            ProgrammStartStop();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void ellBall_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_animationsTimer.IsEnabled)
            {
                zaehler += 1;
                lblScore.Content = $"HITS {zaehler}";
            }
        }

        private void mnu_StartStop_Click(object sender, RoutedEventArgs e)
        {
            ProgrammStartStop();
        }

        private void mnu_Hits_anzeigen_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(lblScore.Content.ToString(), "HITS");
        }

        private void mnuBeenden_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            ProgrammStartStop();
        }

        private void ProgrammStartStop()
        {
            if (_animationsTimer.IsEnabled)
            {
                _animationsTimer.Stop();
            }
            else
            {
                _animationsTimer.Start();
                zaehler = 0;
                lblScore.Content = $"HITS {zaehler}";
            }
        }
    }
}
