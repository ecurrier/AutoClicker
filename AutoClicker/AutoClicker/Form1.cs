using Gma.UserActivityMonitor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace AutoClicker
{
    public partial class autoClickerForm : Form
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        Stopwatch stopwatch = new Stopwatch();
        List<int> clickXCoordinates = new List<int>();
        List<int> clickYCoordinates = new List<int>();
        List<long> clickTimes = new List<long>();
        
        BackgroundWorker backgroundWorker = new BackgroundWorker();

        public autoClickerForm()
        {
            InitializeComponent();

            backgroundWorker.DoWork += backgroundworker_DoWork;
            backgroundWorker.ProgressChanged += backgroundworker_ProgressChanged;
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
        }

        private void HookManager_MouseDown(object sender, MouseEventArgs e)
        {
            clickXCoordinates.Add(e.X);
            clickYCoordinates.Add(e.Y);
            clickTimes.Add(stopwatch.ElapsedMilliseconds);

            var clickLogItem = new ListViewItem(e.X.ToString())
            {
                SubItems =
                {
                    e.Y.ToString(),
                    $"{stopwatch.Elapsed.Minutes}:{stopwatch.Elapsed.Seconds}:{stopwatch.Elapsed.Milliseconds}"
                }
            };

            clickLog.Items.Add(clickLogItem);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            timer.Interval = 10;
            timer.Tick += new EventHandler(timerTick);
            timer.Start();
            
            stopwatch.Start();

            // Hook MouseDown function
            HookManager.MouseDown += HookManager_MouseDown;
        }

        private void endButton_Click(object sender, EventArgs e)
        {
            // Unhook MouseDown function
            HookManager.MouseDown -= HookManager_MouseDown;

            // Remove the click recorded for ending the recording
            clickXCoordinates.RemoveAt(clickXCoordinates.Count - 1);
            clickYCoordinates.RemoveAt(clickYCoordinates.Count - 1);
            clickTimes.RemoveAt(clickTimes.Count - 1);
            clickLog.Items.RemoveAt(clickLog.Items.Count - 1);

            timer.Stop();

            stopwatch.Reset();
            stopwatch.Stop();
        }

        private void timerTick(object sender, EventArgs e)
        {
            var elapsed = stopwatch.Elapsed;
            timerText.Text = $"{elapsed.Minutes}:{elapsed.Seconds}:{elapsed.Milliseconds}";
        }

        public void MouseClick(int x, int y)
        {
            Cursor.Position = new Point(x, y);

            Thread.Sleep(250);

            mouse_event(MOUSEEVENTF_LEFTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
        }

        private void replayButton_Click(object sender, EventArgs e)
        {
            progressBar.Maximum = clickTimes.Count;

            backgroundWorker.RunWorkerAsync();
        }

        private void backgroundworker_DoWork(object sender, DoWorkEventArgs e)
        {
            var playbackStopWatch = new Stopwatch();
            playbackStopWatch.Start();

            while (true)
            {
                int i = 0;
                while (i < clickTimes.Count)
                {
                    if (backgroundWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    if (clickTimes[i] <= playbackStopWatch.ElapsedMilliseconds)
                    {
                        playbackStopWatch.Stop();
                        MouseClick(clickXCoordinates[i], clickYCoordinates[i]);
                        i++;
                        backgroundWorker.ReportProgress(i);
                        playbackStopWatch.Start();
                    }
                }
            }
        }

        private void backgroundworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            clickXCoordinates.Clear();
            clickYCoordinates.Clear();
            clickTimes.Clear();
            clickLog.Items.Clear();
        }

        private void stopRecordingButton_Click(object sender, EventArgs e)
        {
            backgroundWorker.CancelAsync();
        }
    }
}
