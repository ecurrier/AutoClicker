using Gma.UserActivityMonitor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace AutoClicker
{
    public partial class autoClickerForm : Form
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        Stopwatch stopwatch = new Stopwatch();
        BackgroundWorker backgroundWorker = new BackgroundWorker();
        List<ClickRecord> clicks = new List<ClickRecord>();

        public autoClickerForm()
        {
            InitializeComponent();

            // Initialize timer attributes
            timer.Interval = 10;
            timer.Tick += new EventHandler(timerTick);

            // Initalize background worker attributes
            backgroundWorker.DoWork += backgroundworker_DoWork;
            backgroundWorker.ProgressChanged += backgroundworker_ProgressChanged;
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
        }

        private void HookManager_MouseDown(object sender, MouseEventArgs e)
        {
            clicks.Add(new ClickRecord()
            {
                x = e.X,
                y = e.Y,
                time = stopwatch.ElapsedMilliseconds
            });

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
            clicks.RemoveAt(clicks.Count - 1);
            clickLog.Items.RemoveAt(clickLog.Items.Count - 1);

            timer.Stop();
            stopwatch.Reset();
        }

        private void timerTick(object sender, EventArgs e)
        {
            var elapsed = stopwatch.Elapsed;
            timerText.Text = $"{elapsed.Minutes}:{elapsed.Seconds}:{elapsed.Milliseconds}";
        }

        private void mouseClick(int x, int y)
        {
            Cursor.Position = new Point(x, y);

            // Momentarily sleep to allow time for game to recognize object below cursor
            Thread.Sleep(250);

            mouse_event(MOUSEEVENTF_LEFTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
        }

        private void replayButton_Click(object sender, EventArgs e)
        {
            progressBar.Maximum = clicks.Count;
            backgroundWorker.RunWorkerAsync();
        }

        private void backgroundworker_DoWork(object sender, DoWorkEventArgs e)
        {
            var playbackStopWatch = new Stopwatch();
            while (true)
            {
                var i = 0;
                playbackStopWatch.Restart();

                while (i < clicks.Count)
                {
                    if (backgroundWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    if (clicks[i].time <= playbackStopWatch.ElapsedMilliseconds)
                    {
                        // Stop the stopwatch while click is performed since stopwatch continues running in the background
                        playbackStopWatch.Stop();

                        mouseClick(clicks[i].x, clicks[i].y);
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
            clicks.Clear();
            clickLog.Items.Clear();
        }

        private void stopRecordingButton_Click(object sender, EventArgs e)
        {
            backgroundWorker.CancelAsync();
        }

        private void exportRecordingButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog()
            {
                FileName = "default.txt",
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(savefile.FileName))
                {
                    sw.WriteLine("x,y,time");
                    for (var i = 0; i < clicks.Count; i++) {
                        sw.WriteLine($"{clicks[i].x},{clicks[i].y},{clicks[i].time}");
                    }
                }
            }
        }

        private void importRecordingButton_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            var fileName = openFileDialog1.FileName;

            List<ClickRecord> importedClicks = File.ReadAllLines(fileName).Skip(1).Select(v => ClickRecord.FromCsv(v)).ToList();
            foreach(var click in importedClicks)
            {
                clicks.Add(new ClickRecord()
                {
                    x = click.x,
                    y = click.y,
                    time = click.time
                });

                var timeSpan = TimeSpan.FromMilliseconds(click.time);
                var clickLogItem = new ListViewItem(click.x.ToString())
                {
                    SubItems =
                    {
                        click.y.ToString(),
                        $"{timeSpan.Minutes}:{timeSpan.Seconds}:{timeSpan.Milliseconds}"
                    }
                };

                clickLog.Items.Add(clickLogItem);
            }
        }
    }

    public class ClickRecord
    {
        public int x;
        public int y;
        public long time;

        public static ClickRecord FromCsv(string line)
        {
            string[] values = line.Split(',');
            ClickRecord clickRecord = new ClickRecord()
            {
                x = Convert.ToInt32(values[0]),
                y = Convert.ToInt32(values[1]),
                time = Convert.ToInt64(values[2])
            };

            return clickRecord;
        }
    }
}
