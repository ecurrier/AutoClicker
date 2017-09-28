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

        [DllImport("user32.dll")]
        private static extern short VkKeyScan(char ch);

        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;

        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;

        private int autoChatterMinuteCheck = 15;

        private bool recordingActions = false;
        private bool nextActionPrecise = false;
        private bool runInfinitely = false;

        private System.Windows.Forms.Timer recordingTimer = new System.Windows.Forms.Timer();
        private Stopwatch recordingStopwatch = new Stopwatch();
        private Stopwatch autoChatterStopwatch = new Stopwatch();
        private BackgroundWorker playbackThread = new BackgroundWorker();
        private List<EventRecord> recordingEvents = new List<EventRecord>();

        private List<string> Conversations = new List<string>
        {
            "sup sup",
            "runecrafting levels",
            "hello everyone",
            "what a lovely day"
        };

        public autoClickerForm()
        {
            InitializeComponent();

            // Default Run Infinitely? Checkbox to true
            runInfinitelyCheckBox.Checked = true;

            // Initialize timer attributes
            recordingTimer.Interval = 10;
            recordingTimer.Tick += new EventHandler(timerTick);

            // Initialize Auto Chatter Stopwatch
            autoChatterStopwatch.Start();

            // Initalize background worker attributes
            playbackThread.DoWork += PlaybackRecording;
            playbackThread.WorkerReportsProgress = true;
            playbackThread.WorkerSupportsCancellation = true;

            // Hook MouseDown and KeyDown functions
            HookManager.MouseDown += HandleMouseDown;
            HookManager.KeyDown += HandleKeyDown;
        }

        private void HandleMouseDown(object sender, MouseEventArgs e)
        {
            if (!recordingActions)
            {
                return;
            }

            var leftClick = true;
            if (e.Button == MouseButtons.Right)
            {
                leftClick = false;
            }

            LogMouseDownEvent(1, e.X, e.Y, leftClick, recordingStopwatch.ElapsedMilliseconds, nextActionPrecise);

            if (nextActionPrecise)
            {
                ResetNextActionPrecise();
            }
        }

        private void LogMouseDownEvent(int eventType, int xCoord, int yCoord, bool leftClick, long time, bool preciseClick)
        {
            var timeSpan = TimeSpan.FromMilliseconds(time);
            recordingEvents.Add(new EventRecord()
            {
                eventType = eventType,
                x = xCoord,
                y = yCoord,
                leftClick = leftClick,
                time = time,
                preciseClick = preciseClick
            });

            string action = leftClick ? "Left Click" : "Right Click";
            string preciseClickLabel = preciseClick ? "Yes" : "No"; 
            var eventLogItem = new ListViewItem(action)
            {
                SubItems =
                {
                    $"({xCoord},{yCoord})",
                    $"{timeSpan.Minutes}:{timeSpan.Seconds}:{timeSpan.Milliseconds}",
                    preciseClickLabel
                }
            };

            eventLog.Items.Add(eventLogItem);
        }

        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    StartPlayback();
                    return;
                case Keys.F2:
                    EndPlayback();
                    return;
            }

            if (!recordingActions)
            {
                return;
            }

            LogKeyDownEvent(2, (int)e.KeyCode, e.KeyCode.ToString(), recordingStopwatch.ElapsedMilliseconds);
        }

        private void LogKeyDownEvent(int eventType, int keyCode, string keyLabel, long time)
        {
            var timeSpan = TimeSpan.FromMilliseconds(time);
            recordingEvents.Add(new EventRecord()
            {
                eventType = eventType,
                keyCode = keyCode,
                keyLabel = keyLabel,
                time = time
            });

            var eventLogItem = new ListViewItem("Key Press")
            {
                SubItems =
                {
                    $"{keyLabel}",
                    $"{timeSpan.Minutes}:{timeSpan.Seconds}:{timeSpan.Milliseconds}",
                    "No"
                }
            };

            eventLog.Items.Add(eventLogItem);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            recordingTimer.Start();
            recordingStopwatch.Start();
            recordingActions = true;
        }

        private void endButton_Click(object sender, EventArgs e)
        {
            if (!recordingActions)
            {
                return;
            }

            recordingActions = false;

            // Remove the click recorded for ending the recording
            recordingEvents.RemoveAt(recordingEvents.Count - 1);
            eventLog.Items.RemoveAt(eventLog.Items.Count - 1);

            recordingTimer.Stop();
            recordingStopwatch.Reset();
        }

        private void timerTick(object sender, EventArgs e)
        {
            var elapsed = recordingStopwatch.Elapsed;
            timerText.Text = $"{elapsed.Minutes}:{elapsed.Seconds}:{elapsed.Milliseconds}";
        }

        private void mouseClick(int x, int y, bool leftClick, bool preciseClick)
        {
            if (!preciseClick)
            {
                var xOffset = new Random().Next(-5, 6);
                var yOffset = new Random().Next(-5, 6);

                x = x + xOffset;
                y = y + yOffset;
            }

            Cursor.Position = new Point(x, y);
            
            Thread.Sleep(new Random().Next(150, 451));

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
            StartPlayback();
        }

        private void StartPlayback()
        {
            progressBar.Maximum = (int)recordingEvents.Last().time;
            playbackThread.RunWorkerAsync();
        }

        private void PlaybackRecording(object sender, DoWorkEventArgs e)
        {
            var playbackStopWatch = new Stopwatch();
            var loopTracker = 0;

            while (true)
            {
                var i = 0;
                playbackStopWatch.Restart();

                while (i < recordingEvents.Count)
                {
                    var recordingEvent = recordingEvents[i];

                    Invoke((MethodInvoker)delegate {
                        UpdateProgressBar((int)playbackStopWatch.ElapsedMilliseconds);
                    });

                    if (playbackThread.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    if (recordingEvent.time <= playbackStopWatch.ElapsedMilliseconds)
                    {
                        // Stop the stopwatch while click is performed since stopwatch continues running in the background
                        playbackStopWatch.Stop();

                        if (recordingEvent.eventType == 1)
                        {
                            mouseClick(recordingEvent.x, recordingEvent.y, recordingEvent.leftClick, recordingEvent.preciseClick);
                        }
                        else if (recordingEvent.eventType == 2)
                        {
                            keyClick(recordingEvent.keyCode);
                        }

                        i++;
                        playbackStopWatch.Start();
                    }
                }
                
                TypeRandomConversation(autoChatterStopwatch);

                loopTracker++;
                Invoke((MethodInvoker)delegate {
                    UpdateFormTitleText(loopTracker.ToString());
                });

                if (!runInfinitely)
                {
                    var currentLoopCount = int.Parse(loopCounterText.Text);
                    if (currentLoopCount == 0)
                    {
                        playbackThread.CancelAsync();
                        break;
                    }

                    currentLoopCount--;
                    Invoke((MethodInvoker)delegate {
                        UpdateLoopCounterTextBox(currentLoopCount.ToString());
                    });
                }
            }
        }

        public void TypeRandomConversation(Stopwatch autoChatterStopwatch)
        {
            if (autoChatterStopwatch.ElapsedMilliseconds < autoChatterMinuteCheck * 60 * 1000)
            {
                return;
            }

            autoChatterMinuteCheck = new Random().Next(10, 21);

            var conversationIndex = new Random().Next(0, Conversations.Count);
            var conversation = Conversations.ElementAt(conversationIndex);

            TypeConversation(conversation);
        }

        public void TypeConversation(string conversation)
        {
            var conversationChars = conversation.ToCharArray();
            foreach(var conversationChar in conversationChars)
            {
                var key = ConvertCharToVirtualKey(conversationChar);
                keyClick((int)key);
                Thread.Sleep(new Random().Next(75, 125));
            } 
        }

        public Keys ConvertCharToVirtualKey(char ch)
        {
            short vkey = VkKeyScan(ch);
            Keys retval = (Keys)(vkey & 0xff);
            int modifiers = vkey >> 8;
            if ((modifiers & 1) != 0) retval |= Keys.Shift;
            if ((modifiers & 2) != 0) retval |= Keys.Control;
            if ((modifiers & 4) != 0) retval |= Keys.Alt;
            return retval;
        }

        public void UpdateFormTitleText(string text)
        {
            Invoke((MethodInvoker)delegate {
                Text = $"Auto Clicker ({text})";
            });
        }

        public void UpdateLoopCounterTextBox(string text)
        {
            Invoke((MethodInvoker)delegate {
                loopCounterText.Text = text;
            });
        }

        public void UpdateProgressBar(int time)
        {
            Invoke((MethodInvoker)delegate
            {
                progressBar.Value = time;
            });
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            recordingEvents.Clear();
            eventLog.Items.Clear();
        }

        private void stopRecordingButton_Click(object sender, EventArgs e)
        {
            EndPlayback();
        }

        private void EndPlayback()
        {
            playbackThread.CancelAsync();
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
                    for (var i = 0; i < recordingEvents.Count; i++)
                    {
                        if (recordingEvents[i].eventType == 1)
                        {
                            sw.WriteLine($"{recordingEvents[i].eventType},{recordingEvents[i].x},{recordingEvents[i].y},{recordingEvents[i].leftClick.ToString()},{recordingEvents[i].time},{recordingEvents[i].preciseClick.ToString()}");
                        }
                        else if (recordingEvents[i].eventType == 2)
                        {
                            sw.WriteLine($"{recordingEvents[i].eventType},{recordingEvents[i].keyCode},{recordingEvents[i].keyLabel},{recordingEvents[i].time}");
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
            foreach (var importedEvent in importedEvents)
            {
                if (importedEvent.eventType == 1)
                {
                    LogMouseDownEvent(1, importedEvent.x, importedEvent.y, importedEvent.leftClick, importedEvent.time, importedEvent.preciseClick);
                }
                else if (importedEvent.eventType == 2)
                {
                    LogKeyDownEvent(importedEvent.eventType, importedEvent.keyCode, importedEvent.keyLabel, importedEvent.time);
                }
            }
        }

        private void runInfinitelyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (runInfinitelyCheckBox.Checked)
            {
                runInfinitely = true;
                loopCounterText.Text = null;
                loopCounterText.ReadOnly = true;
            }
            else
            {
                runInfinitely = false;
                loopCounterText.ReadOnly = false;
            }
        }

        private void preciseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (preciseCheckBox.Checked)
            {
                // Remove the click recorded for checking this box
                recordingEvents.RemoveAt(recordingEvents.Count - 1);
                eventLog.Items.RemoveAt(eventLog.Items.Count - 1);

                nextActionPrecise = true;
            }
        }

        private void ResetNextActionPrecise()
        {
            preciseCheckBox.Checked = false;
            nextActionPrecise = false;
        }
    }

    public class EventRecord
    {
        public int eventType; // Click = 1, Key = 2
        public int x;
        public int y;
        public int keyCode;
        public string keyLabel;
        public bool leftClick;
        public long time;
        public bool preciseClick;

        public static EventRecord FromCsv(string line)
        {
            string[] values = line.Split(',');
            EventRecord eventRecord = new EventRecord()
            {
                eventType = Convert.ToInt32(values[0])
            };

            if (eventRecord.eventType == 1)
            {
                eventRecord.x = Convert.ToInt32(values[1]);
                eventRecord.y = Convert.ToInt32(values[2]);
                eventRecord.leftClick = Convert.ToBoolean(values[3]);
                eventRecord.time = Convert.ToInt64(values[4]);
                eventRecord.preciseClick = Convert.ToBoolean(values[5]);
            }
            else if (eventRecord.eventType == 2)
            {
                eventRecord.keyCode = Convert.ToInt32(values[1]);
                eventRecord.keyLabel = Convert.ToString(values[2]);
                eventRecord.time = Convert.ToInt32(values[3]);
            }

            return eventRecord;
        }
    }
}
