using RemoteControl;
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
using System.IO.Ports;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Threading;
using System.ComponentModel;

namespace RemoteControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    /* 
     * problem: arrowkeys are not working --> WASD (needs to be fixed)
     * W ... Up
     * A ... Left
     * S ... Down
     * D ... Right
     */
    public partial class MainWindow : Window
    {
        KeyCommands keyCommands = new KeyCommands();
        Port ports = new Port();
        ObservableCollection<int> baud = new ObservableCollection<int>();
        DispatcherTimer timer = new DispatcherTimer();

        Thread portwritethread;

        bool start;
        bool doonce;
        bool rightKey;
        bool leftKey;
        bool upKey;
        bool downKey;

        public MainWindow()
        {
            InitializeComponent();
            // TO DO: check if exe already running

            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 50);

            grdCommands.DataContext = keyCommands;
            cbobxPort.DataContext = ports;
            cbobxBaud.DataContext = ports.Baud;

            // Get IO-Ports
            string[] arrports = SerialPort.GetPortNames();

            foreach (string port in arrports)
            {
                ports.Add(port);
            }

            // Setup Baudrate
            ports.addBaudrate = 9600;
            ports.addBaudrate = 38400;
            ports.addBaudrate = 115200;

        }
        // TODO: Destroy/Stop Threading

        private void Timer_Tick(object sender, EventArgs e)
        {
            txtblOutput.Text += ports.Data + Environment.NewLine;
            scrlvOut.ScrollToEnd();
            SetKeyImageUpdateData();
            //ports.Write();
        }

        private void SetKeyImageUpdateData()
        {
            if(upKey)
            {
                imgUpPrs.Visibility = Visibility.Visible;
                if (rightKey)
                {
                    imgRightPrs.Visibility = Visibility.Visible;
                    imgLeftPrs.Visibility = Visibility.Hidden;
                    ports.Data = keyCommands.Rightup;
                    return;
                }
                else if(leftKey)
                {
                    imgRightPrs.Visibility = Visibility.Hidden;
                    imgLeftPrs.Visibility = Visibility.Visible;
                    ports.Data = keyCommands.Leftup;
                    return;
                }
                else
                {
                    ports.Data = keyCommands.Up;
                    imgLeftPrs.Visibility = Visibility.Hidden;
                    imgRightPrs.Visibility = Visibility.Hidden;
                }
                return;
            }
            else
            {
                imgUpPrs.Visibility = Visibility.Hidden;
            }

            if (downKey)
            {
                imgDownPrs.Visibility = Visibility.Visible;
                if (rightKey)
                {
                    imgRightPrs.Visibility = Visibility.Visible;
                    imgLeftPrs.Visibility = Visibility.Hidden;
                    ports.Data = keyCommands.Rightdown;
                    return;
                }
                else if (leftKey)
                {
                    imgRightPrs.Visibility = Visibility.Hidden;
                    imgLeftPrs.Visibility = Visibility.Visible;
                    ports.Data = keyCommands.Leftdown;
                    return;
                }
                else
                {
                    ports.Data = keyCommands.Down;
                    imgRightPrs.Visibility = Visibility.Hidden;
                    imgLeftPrs.Visibility = Visibility.Hidden;
                }
                return;
            }
            else
            {
                imgDownPrs.Visibility = Visibility.Hidden;
            }

            if (rightKey && !upKey && !downKey)
            {
                imgRightPrs.Visibility = Visibility.Visible;
                ports.Data = keyCommands.Right;
                return;
            }
            else
            {
                imgRightPrs.Visibility = Visibility.Hidden;
            }

            if (leftKey && !upKey && !downKey)
            {
                imgLeftPrs.Visibility = Visibility.Visible;
                ports.Data = keyCommands.Left;
                return;
            }
            else
            {
                imgLeftPrs.Visibility = Visibility.Hidden;
            }
            ports.Data = keyCommands.Stop;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (rdbOdd.IsChecked == true)
            {
                ports.Selectedparity = Parity.Odd;
            }
            else if (rdbEven.IsChecked == true)
            {
                ports.Selectedparity = Parity.Even;
            }
            else
            {
                ports.Selectedparity = Parity.None;
            }
            ports.Data = keyCommands.Stop; // for first transfer

            try
            {
                if (!start && doonce == false)
                {
                    doonce = true;
                    // new Thread for ports.write();
                    portwritethread = new Thread(new ThreadStart(ports.Write));
                    start = ports.Start();
                    ports.serialportstarted = start;
                    portwritethread.Start();
                    timer.Start();
                }
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message + "\nApplication will be closed!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
               Environment.Exit(exitCode: 0);
               this.Close();
               return;
               
            }
           
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (start)
                {
                    doonce = false;
                    // Close Thread
                    start = false;
                    ports.serialportstarted = start;
                    portwritethread.Abort();


                    start = ports.Stop();
                    timer.Stop();
                    imgRightPrs.Visibility = Visibility.Hidden;
                    imgLeftPrs.Visibility = Visibility.Hidden;
                    imgUpPrs.Visibility = Visibility.Hidden;
                    imgDownPrs.Visibility = Visibility.Hidden;

                    rightKey = false;
                    leftKey = false;
                    upKey = false;
                    downKey = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nApplication will be closed!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(exitCode: 0);
                this.Close();
                return;
            }
        }
        private void cbobxBaud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ports.Selectedbaud = (uint)cbobxBaud.SelectedItem;
        }

        private void cbobxPort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ports.Selectedport = cbobxPort.SelectedItem as string;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    upKey = true;
                    break;
                case Key.S:
                    downKey = true;
                    break;
                case Key.D:
                    rightKey = true;
                    break;
                case Key.A:
                    leftKey = true;
                    break;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    upKey = false;
                    break;
                case Key.S:
                    downKey = false;
                    break;
                case Key.D:
                    rightKey = false;
                    break;
                case Key.A:
                    leftKey = false;
                    break;
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            MessageBox.Show("The application is closing! " +
                "\nThe writing process is going to be stopped!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            Environment.Exit(0);
            this.Close();
        }
    }

}
