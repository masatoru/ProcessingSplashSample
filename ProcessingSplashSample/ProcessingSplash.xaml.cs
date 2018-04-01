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
//        Action ProcessAction;
        Action<string> ProcessFunc;
        public bool IsComplete { get; private set; }
        public bool IsClose { get; private set; }

        public ProcessingSplash(string message, Action<string> processFunc,List<string> lst)
        {
            InitializeComponent();
            this.ProcessFunc = processFunc;
            DataContext = message;
            IsComplete = false;
            IsClose = false;

            _backgroundWorker.DoWork += _backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.RunWorkerAsync(lst);
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (ProcessFunc != null)
            {
                var lst = (List<string>) e.Argument;
                foreach (var path in lst.Select((v,i) => new{v,i}))
                {
                    Dispatcher.Invoke(() =>
                    {
                        progressbar.Maximum = lst.Count;
                        progressbar.Value = path.i + 1;
                        status.Content = $"{path.i+1}/{lst.Count}:{path.v}";
                    });

                    ProcessFunc.Invoke("a");

                    // キャンセルされてないか定期的にチェック
                    if (_backgroundWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                }
            }
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
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

        /// <summary>
        /// キャンセル処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCancelOnClick(object sender, RoutedEventArgs e)
        {
            _backgroundWorker.CancelAsync();
        }
    }
}