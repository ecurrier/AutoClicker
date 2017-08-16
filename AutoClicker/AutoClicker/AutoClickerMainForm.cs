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

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;

        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        Stopwatch stopwatch = new Stopwatch();
        BackgroundWorker backgroundWorker = new BackgroundWorker();
        List<EventRecord> events = new List<EventRecord>();

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
            bool leftClick = true;
            if(e.Button == MouseButtons.Right)
            {
                leftClick = false;
            }

            events.Add(new EventRecord()
            {
                eventType = 1,
                x = e.X,
                y = e.Y,
                leftClick = leftClick,
                time = stopwatch.ElapsedMilliseconds
            });

            var eventLogItem = new ListViewItem(e.X.ToString())
            {
                SubItems =
                {
                    e.Y.ToString(),
                    $"{stopwatch.Elapsed.Minutes}:{stopwatch.Elapsed.Seconds}:{stopwatch.Elapsed.Milliseconds}"
                }
            };

            eventLog.Items.Add(eventLogItem);
        }

        private void HookManager_KeyDown(object sender, KeyEventArgs e)
        {
            events.Add(new EventRecord()
            {
                eventType = 2,
                keyCode = (int)e.KeyCode,
                time = stopwatch.ElapsedMilliseconds
            });

            var eventLogItem = new ListViewItem(e.KeyCode.ToString())
            {
                SubItems =
                {
                    $"{stopwatch.Elapsed.Minutes}:{stopwatch.Elapsed.Seconds}:{stopwatch.Elapsed.Milliseconds}"
                }
            };

            eventLog.Items.Add(eventLogItem);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            timer.Start();
            stopwatch.Start();

            // Hook MouseDown and KeyDown functions
            HookManager.MouseDown += HookManager_MouseDown;
            HookManager.KeyDown += HookManager_KeyDown;
        }

        private void endButton_Click(object sender, EventArgs e)
        {
            // Unhook MouseDown and KeyDown functions
            HookManager.MouseDown -= HookManager_MouseDown;
            HookManager.KeyDown -= HookManager_KeyDown;

            // Remove the click recorded for ending the recording
            events.RemoveAt(events.Count - 1);
            eventLog.Items.RemoveAt(eventLog.Items.Count - 1);

            timer.Stop();
            stopwatch.Reset();
        }

        private void timerTick(object sender, EventArgs e)
        {
            var elapsed = stopwatch.Elapsed;
            timerText.Text = $"{elapsed.Minutes}:{elapsed.Seconds}:{elapsed.Milliseconds}";
        }

        private void mouseClick(int x, int y, bool leftClick)
        {
            Cursor.Position = new Point(x, y);

            // Momentarily sleep to allow time for game to recognize object below cursor
            Thread.Sleep(250);

            if (leftClick)
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
            }
            else
            {
                mouse_event(MOUSEEVENTF_RIGHTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                mouse_event(MOUSEEVENTF_RIGHTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
            }
        }

        private void keyClick(int keyCode)
        {
            keybd_event(Convert.ToByte(keyCode), 0, 1 | 0, 0);
        }

        private void replayButton_Click(object sender, EventArgs e)
        {
            progressBar.Maximum = events.Count;
            backgroundWorker.RunWorkerAsync();
        }

        private void backgroundworker_DoWork(object sender, DoWorkEventArgs e)
        {
            var playbackStopWatch = new Stopwatch();
            while (true)
            {
                var i = 0;
                playbackStopWatch.Restart();

                while (i < events.Count)
                {
                    if (backgroundWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    if (events[i].time <= playbackStopWatch.ElapsedMilliseconds)
                    {
                        // Stop the stopwatch while click is performed since stopwatch continues running in the background
                        playbackStopWatch.Stop();

                        if (events[i].eventType == 1)
                        {
                            mouseClick(events[i].x, events[i].y, events[i].leftClick);
                        }
                        else if(events[i].eventType == 2)
                        {
                            keyClick(events[i].keyCode);
                        }

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
            events.Clear();
            eventLog.Items.Clear();
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
                    for (var i = 0; i < events.Count; i++) {
                        if (events[i].eventType == 1)
                        {
                            sw.WriteLine($"{events[i].eventType},{events[i].x},{events[i].y},{events[i].leftClick.ToString()},{events[i].time}");
                        }
                        else if(events[i].eventType == 2)
                        {
                            sw.WriteLine($"{events[i].eventType},{events[i].keyCode},{events[i].time}");
                        }
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

            List<EventRecord> importedEvents = File.ReadAllLines(fileName).Select(v => EventRecord.FromCsv(v)).ToList();
            foreach (var _event in importedEvents)
            {
                if (_event.eventType == 1)
                {
                    events.Add(new EventRecord()
                    {
                        eventType = _event.eventType,
                        x = _event.x,
                        y = _event.y,
                        leftClick = _event.leftClick,
                        time = _event.time
                    });

                    var timeSpan = TimeSpan.FromMilliseconds(_event.time);
                    var eventLogItem = new ListViewItem(_event.x.ToString())
                    {
                        SubItems =
                        {
                            _event.y.ToString(),
                            $"{timeSpan.Minutes}:{timeSpan.Seconds}:{timeSpan.Milliseconds}"
                        }
                    };

                    eventLog.Items.Add(eventLogItem);
                }
                else if(_event.eventType == 2)
                {
                    events.Add(new EventRecord()
                    {
                        eventType = _event.eventType,
                        keyCode = _event.keyCode,
                        time = _event.time
                    });

                    var timeSpan = TimeSpan.FromMilliseconds(_event.time);
                    var eventLogItem = new ListViewItem(_event.keyCode.ToString())
                    {
                        SubItems =
                        {
                            $"{timeSpan.Minutes}:{timeSpan.Seconds}:{timeSpan.Milliseconds}"
                        }
                    };

                    eventLog.Items.Add(eventLogItem);
                }
            }
        }
    }

    public class EventRecord
    {
        public int eventType; // Click = 1, Key = 2
        public int x;
        public int y;
        public int keyCode;
        public bool leftClick;
        public long time;

        public static EventRecord FromCsv(string line)
        {
            string[] values = line.Split(',');
            EventRecord eventRecord = new EventRecord()
            {
                eventType = Convert.ToInt32(values[0])
            };

            if(eventRecord.eventType == 1)
            {
                eventRecord.x = Convert.ToInt32(values[1]);
                eventRecord.y = Convert.ToInt32(values[2]);
                eventRecord.leftClick = Convert.ToBoolean(values[3]);
                eventRecord.time = Convert.ToInt64(values[4]);
            }
            else if(eventRecord.eventType == 2)
            {
                eventRecord.keyCode = Convert.ToInt32(values[1]);
                eventRecord.time = Convert.ToInt32(values[2]);
            }

            return eventRecord;
        }
    }
}
