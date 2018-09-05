using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace WindowTimer
{
    public partial class MainWindow : Window
    {
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
    
            BackgroundWorker bw = sender as BackgroundWorker;

            if (bw.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            while (!bw.CancellationPending)
            {
                App.Monitoring();
            }
           
        }

        private void backgroundWorker1_RunWorkerCompleted(
            object sender,
            RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                
                // The user canceled the operation.
            }
            else if (e.Error != null)
            {
                // There was an error during the operation.
                string msg = String.Format("An error occurred: {0}", e.Error.Message);
                System.Windows.MessageBox.Show(msg);
            }
            else
            {
                // The operation completed normally.
                
            }
        }

        // This method models an operation that may take a long time 
        // to run. It can be cancelled, it can raise an exception,
        // or it can exit normally and return a result. 
        

        

        

    }
}
