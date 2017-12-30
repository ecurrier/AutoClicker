namespace AutoClicker
{
    partial class autoClickerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.startButton = new System.Windows.Forms.Button();
            this.endButton = new System.Windows.Forms.Button();
            this.replayButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.timerLabel = new System.Windows.Forms.Label();
            this.timerText = new System.Windows.Forms.TextBox();
            this.eventLog = new System.Windows.Forms.ListView();
            this.action = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.detail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timeOccurred = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.precise = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.stopRecordingButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.exportRecordingButton = new System.Windows.Forms.Button();
            this.importRecordingButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.loopCounterText = new System.Windows.Forms.TextBox();
            this.runInfinitelyCheckBox = new System.Windows.Forms.CheckBox();
            this.preciseCheckBox = new System.Windows.Forms.CheckBox();
            this.suspendPlaybackButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(18, 18);
            this.startButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(172, 85);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start Recording";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // endButton
            // 
            this.endButton.Location = new System.Drawing.Point(202, 18);
            this.endButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.endButton.Name = "endButton";
            this.endButton.Size = new System.Drawing.Size(172, 85);
            this.endButton.TabIndex = 1;
            this.endButton.Text = "End Recording";
            this.endButton.UseVisualStyleBackColor = true;
            this.endButton.Click += new System.EventHandler(this.endButton_Click);
            // 
            // replayButton
            // 
            this.replayButton.Location = new System.Drawing.Point(18, 331);
            this.replayButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.replayButton.Name = "replayButton";
            this.replayButton.Size = new System.Drawing.Size(172, 40);
            this.replayButton.TabIndex = 3;
            this.replayButton.Text = "Start Playback (F1)";
            this.replayButton.UseVisualStyleBackColor = true;
            this.replayButton.Click += new System.EventHandler(this.replayButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(284, 425);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(88, 37);
            this.progressBar.TabIndex = 5;
            // 
            // timerLabel
            // 
            this.timerLabel.AutoSize = true;
            this.timerLabel.Location = new System.Drawing.Point(21, 118);
            this.timerLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(47, 20);
            this.timerLabel.TabIndex = 6;
            this.timerLabel.Text = "Time:";
            // 
            // timerText
            // 
            this.timerText.Location = new System.Drawing.Point(80, 114);
            this.timerText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.timerText.Name = "timerText";
            this.timerText.Size = new System.Drawing.Size(109, 26);
            this.timerText.TabIndex = 7;
            // 
            // eventLog
            // 
            this.eventLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.action,
            this.detail,
            this.timeOccurred,
            this.precise});
            this.eventLog.Location = new System.Drawing.Point(18, 162);
            this.eventLog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.eventLog.Name = "eventLog";
            this.eventLog.Size = new System.Drawing.Size(355, 158);
            this.eventLog.TabIndex = 8;
            this.eventLog.UseCompatibleStateImageBehavior = false;
            this.eventLog.View = System.Windows.Forms.View.Details;
            // 
            // action
            // 
            this.action.Text = "Action";
            this.action.Width = 45;
            // 
            // detail
            // 
            this.detail.Text = "Detail";
            this.detail.Width = 45;
            // 
            // timeOccurred
            // 
            this.timeOccurred.Text = "Time";
            this.timeOccurred.Width = 89;
            // 
            // precise
            // 
            this.precise.Text = "Precise?";
            this.precise.Width = 55;
            // 
            // stopRecordingButton
            // 
            this.stopRecordingButton.Location = new System.Drawing.Point(202, 331);
            this.stopRecordingButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.stopRecordingButton.Name = "stopRecordingButton";
            this.stopRecordingButton.Size = new System.Drawing.Size(172, 40);
            this.stopRecordingButton.TabIndex = 9;
            this.stopRecordingButton.Text = "End Playblack (F2)";
            this.stopRecordingButton.UseVisualStyleBackColor = true;
            this.stopRecordingButton.Click += new System.EventHandler(this.stopRecordingButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(202, 112);
            this.resetButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(172, 37);
            this.resetButton.TabIndex = 10;
            this.resetButton.Text = "Clear Actions";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // exportRecordingButton
            // 
            this.exportRecordingButton.Location = new System.Drawing.Point(200, 474);
            this.exportRecordingButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.exportRecordingButton.Name = "exportRecordingButton";
            this.exportRecordingButton.Size = new System.Drawing.Size(172, 46);
            this.exportRecordingButton.TabIndex = 11;
            this.exportRecordingButton.Text = "Export Recording";
            this.exportRecordingButton.UseVisualStyleBackColor = true;
            this.exportRecordingButton.Click += new System.EventHandler(this.exportRecordingButton_Click);
            // 
            // importRecordingButton
            // 
            this.importRecordingButton.Location = new System.Drawing.Point(200, 529);
            this.importRecordingButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.importRecordingButton.Name = "importRecordingButton";
            this.importRecordingButton.Size = new System.Drawing.Size(172, 46);
            this.importRecordingButton.TabIndex = 12;
            this.importRecordingButton.Text = "Import Recording";
            this.importRecordingButton.UseVisualStyleBackColor = true;
            this.importRecordingButton.Click += new System.EventHandler(this.importRecordingButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(198, 432);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "Progress:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 543);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 20);
            this.label2.TabIndex = 14;
            this.label2.Text = "Loops:";
            // 
            // loopCounterText
            // 
            this.loopCounterText.Location = new System.Drawing.Point(80, 538);
            this.loopCounterText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.loopCounterText.Name = "loopCounterText";
            this.loopCounterText.Size = new System.Drawing.Size(102, 26);
            this.loopCounterText.TabIndex = 15;
            // 
            // runInfinitelyCheckBox
            // 
            this.runInfinitelyCheckBox.AutoSize = true;
            this.runInfinitelyCheckBox.Location = new System.Drawing.Point(44, 486);
            this.runInfinitelyCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.runInfinitelyCheckBox.Name = "runInfinitelyCheckBox";
            this.runInfinitelyCheckBox.Size = new System.Drawing.Size(136, 24);
            this.runInfinitelyCheckBox.TabIndex = 16;
            this.runInfinitelyCheckBox.Text = "Run Infinitely?";
            this.runInfinitelyCheckBox.UseVisualStyleBackColor = true;
            this.runInfinitelyCheckBox.CheckedChanged += new System.EventHandler(this.runInfinitelyCheckBox_CheckedChanged);
            // 
            // preciseCheckBox
            // 
            this.preciseCheckBox.AutoSize = true;
            this.preciseCheckBox.Location = new System.Drawing.Point(42, 435);
            this.preciseCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.preciseCheckBox.Name = "preciseCheckBox";
            this.preciseCheckBox.Size = new System.Drawing.Size(145, 24);
            this.preciseCheckBox.TabIndex = 17;
            this.preciseCheckBox.Text = "Precise Action?";
            this.preciseCheckBox.UseVisualStyleBackColor = true;
            this.preciseCheckBox.CheckedChanged += new System.EventHandler(this.preciseCheckBox_CheckedChanged);
            // 
            // suspendPlaybackButton
            // 
            this.suspendPlaybackButton.Location = new System.Drawing.Point(18, 377);
            this.suspendPlaybackButton.Name = "suspendPlaybackButton";
            this.suspendPlaybackButton.Size = new System.Drawing.Size(354, 40);
            this.suspendPlaybackButton.TabIndex = 18;
            this.suspendPlaybackButton.Text = "Suspend Playback (F3)";
            this.suspendPlaybackButton.UseVisualStyleBackColor = true;
            this.suspendPlaybackButton.Click += new System.EventHandler(this.suspendPlaybackButton_Click);
            // 
            // autoClickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 594);
            this.Controls.Add(this.suspendPlaybackButton);
            this.Controls.Add(this.preciseCheckBox);
            this.Controls.Add(this.runInfinitelyCheckBox);
            this.Controls.Add(this.loopCounterText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.importRecordingButton);
            this.Controls.Add(this.exportRecordingButton);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.stopRecordingButton);
            this.Controls.Add(this.eventLog);
            this.Controls.Add(this.timerText);
            this.Controls.Add(this.timerLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.replayButton);
            this.Controls.Add(this.endButton);
            this.Controls.Add(this.startButton);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "autoClickerForm";
            this.Text = "Auto Clicker";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button endButton;
        private System.Windows.Forms.Button replayButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label timerLabel;
        private System.Windows.Forms.TextBox timerText;
        private System.Windows.Forms.ListView eventLog;
        private System.Windows.Forms.ColumnHeader timeOccurred;
        private System.Windows.Forms.Button stopRecordingButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button exportRecordingButton;
        private System.Windows.Forms.Button importRecordingButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox loopCounterText;
        private System.Windows.Forms.CheckBox runInfinitelyCheckBox;
        private System.Windows.Forms.CheckBox preciseCheckBox;
        private System.Windows.Forms.ColumnHeader action;
        private System.Windows.Forms.ColumnHeader detail;
        private System.Windows.Forms.ColumnHeader precise;
        private System.Windows.Forms.Button suspendPlaybackButton;
    }
}

