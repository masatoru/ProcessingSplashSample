using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

namespace ProcessingSplashSample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class ProcessingSplash : Window
    {

        BackgroundWorker _backgroundWorker = new BackgroundWorker();
        Action ProcessAction;
        public bool IsComplete { get; private set; }
        public bool IsClose { get; private set; }

        public ProcessingSplash(string message, Action processAction)
        {
            InitializeComponent();
            this.ProcessAction = processAction;
            DataContext = message;
            IsComplete = false;
            IsClose = false;

            _backgroundWorker.DoWork += _backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.RunWorkerAsync();
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (ProcessAction != null)
                ProcessAction.Invoke();
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _backgroundWorker.CancelAsync();
            if (e.Cancelled)
            {
                //Cancelled
                IsComplete = false;
                IsClose = true;
            }
            else if (e.Error != null)
            {
                //Exception Thrown
                IsComplete = false;
                IsClose = true;
            }
            else
            {
                //Completed
                IsComplete = true;
                IsClose = true;
            }

            Dispatcher.Invoke(new Action(() => { this.Close(); }));
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!IsClose)
                e.Cancel = true;
        }
    }
}