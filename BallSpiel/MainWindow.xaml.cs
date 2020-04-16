using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace BallSpiel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Initialize Variables
        private readonly DispatcherTimer _animationsTimer = new DispatcherTimer();
        private bool ellBallDriftingRight = true;
        private bool ellBallDriftingDown = true;
        private int zaehler = 0;
        private int ellBallColor = 1;

        private void ProgrammStartStop()
        {
            // Start or Stop the Game; depending on actual state
            if (_animationsTimer.IsEnabled)
            {
                _animationsTimer.Stop();
            }
            else
            {
                _animationsTimer.Start();
                zaehler = 0;
                lblScore.Content = $"TREFFER: {zaehler}";
            }
        }

        private void positioningellBall(object sender, EventArgs e)
        {
            // Movement Routines for the ball
            var x = Canvas.GetLeft(ellBall);
            var y = Canvas.GetTop(ellBall);

            // Horizontal Axis Movement
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
            // Vertical Axis Movement
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
        
        public MainWindow()
        {
            // Initialize Main Window
            InitializeComponent();

            // Initialize the animations timers for ball 
            _animationsTimer.Interval = TimeSpan.FromMilliseconds(50);
            _animationsTimer.Tick += positioningellBall;
        }

        private void mnu_StartStop_Click(object sender, RoutedEventArgs e)
        {
            // Menu entry, start or stop the Game
            ProgrammStartStop();
        }

        private void mnu_Hits_anzeigen_Click(object sender, RoutedEventArgs e)
        {
            // Menu entry Hits, showing games score in a MessageBox
            MessageBox.Show(lblScore.Content.ToString(), "TREFFER:");
        }

        private void mnuBeenden_Click(object sender, RoutedEventArgs e)
        {
            // Menu entry, stopping the game
            Application.Current.Shutdown();
        }

        private void btnStartStop_Click(object sender, RoutedEventArgs e)
        {
            // Button, start or stop the game
            ProgrammStartStop();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            // Button, stopping the game
            App.Current.Shutdown();
        }

        private void ellBall_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Check if the player hits the ball with a mouse click
            if (_animationsTimer.IsEnabled)
            {
                zaehler += 1;
                lblScore.Content = $"TREFFER: {zaehler}";
            }
        }

        private void BallSpiel_KeyUp(object sender, KeyEventArgs e)
        {
            // START - Change ball color with page up/down keys
            switch (e.Key)
            {
                case Key.PageUp:
                    if (ellBallColor < 4)
                    {
                        ellBallColor = ellBallColor + 1;
                    }
                    else
                    {
                        ellBallColor = 1;
                    }
                    break;
                case Key.PageDown:
                    if (ellBallColor > 1)
                    {
                        ellBallColor = ellBallColor - 1;
                    }
                    else
                    {
                        ellBallColor = 3;
                    }
                    break;
                default:
                    break;
            }
            switch (ellBallColor)
            {
                case 1:
                    ellBall.Fill = Brushes.Blue;
                    break;
                case 2:
                    ellBall.Fill = Brushes.Green;
                    break;
                case 3:
                    ellBall.Fill = Brushes.Red;
                    break;
                default:
                    break;
            }
            // END - Change ball color with page up/down keys
        }
    }
}
