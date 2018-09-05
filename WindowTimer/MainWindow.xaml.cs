using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;



namespace WindowTimer
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NotifyIcon nIcon;

        BackgroundWorker backgroundWorker1 = null;

        public MainWindow()
        {
            InitializeComponent();
            cancelBtn.Visibility = Visibility.Hidden; //Początkowo przycisk stopu jest niewidoczny, będziemy go pokazywać dopiero po starcie
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            nIcon = new NotifyIcon();
            nIcon.Visible = true;
            this.WindowState = WindowState.Minimized;


            this.nIcon.Icon = Properties.Resources.Icon1;
            this.nIcon.Text = "Kliknij podwójnie by wywołać okno!";
            
            this.nIcon.ShowBalloonTip(5000, "Start", "Mierzenie czasu rozpoczęte!", ToolTipIcon.Info);
            
            nIcon.DoubleClick += new EventHandler(notifyIcon1_DoubleClick);
            cancelBtn.Visibility = Visibility.Visible;
            startBtn.Visibility = Visibility.Hidden;

            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.RunWorkerAsync();


        }
        private void notifyIcon1_DoubleClick(object Sender, EventArgs e)
        {
            // Show the form when the user double clicks on the notify icon.

            // Set the WindowState to normal if the form is minimized.
            
            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal;
                this.Activate();
            }
            else WindowState = WindowState.Minimized;

        }

        
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            cancelBtn.Visibility = Visibility.Hidden;
            startBtn.Visibility = Visibility.Visible;
            nIcon.Visible = false;
            backgroundWorker1.CancelAsync();
        }
        

        private void graph1Btn_Click(object sender, RoutedEventArgs e)
        {
            List<myWindow> win = new List<myWindow>();
            foreach (DictionaryEntry pair in App.applhash)
            {
                win.Add(new myWindow() { nazwa = (string)pair.Key, sekundy=(int)pair.Value});
            }
           

            DataGrid1.ItemsSource = win;
            
        }

        private void graph2Btn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        
    }
}
