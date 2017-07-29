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
            this.clickLog = new System.Windows.Forms.ListView();
            this.xCoordinate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.yCoordinate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timeOccurred = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.stopRecordingButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.exportRecordingButton = new System.Windows.Forms.Button();
            this.importRecordingButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(12, 12);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(201, 55);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start Recording";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // endButton
            // 
            this.endButton.Location = new System.Drawing.Point(219, 12);
            this.endButton.Name = "endButton";
            this.endButton.Size = new System.Drawing.Size(189, 55);
            this.endButton.TabIndex = 1;
            this.endButton.Text = "End Recording";
            this.endButton.UseVisualStyleBackColor = true;
            this.endButton.Click += new System.EventHandler(this.endButton_Click);
            // 
            // replayButton
            // 
            this.replayButton.Location = new System.Drawing.Point(12, 417);
            this.replayButton.Name = "replayButton";
            this.replayButton.Size = new System.Drawing.Size(104, 47);
            this.replayButton.TabIndex = 3;
            this.replayButton.Text = "Replay Recording";
            this.replayButton.UseVisualStyleBackColor = true;
            this.replayButton.Click += new System.EventHandler(this.replayButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(237, 417);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(171, 47);
            this.progressBar.TabIndex = 5;
            // 
            // timerLabel
            // 
            this.timerLabel.AutoSize = true;
            this.timerLabel.Location = new System.Drawing.Point(14, 77);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(33, 13);
            this.timerLabel.TabIndex = 6;
            this.timerLabel.Text = "Time:";
            // 
            // timerText
            // 
            this.timerText.Location = new System.Drawing.Point(53, 74);
            this.timerText.Name = "timerText";
            this.timerText.Size = new System.Drawing.Size(100, 20);
            this.timerText.TabIndex = 7;
            // 
            // clickLog
            // 
            this.clickLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.xCoordinate,
            this.yCoordinate,
            this.timeOccurred});
            this.clickLog.Location = new System.Drawing.Point(12, 105);
            this.clickLog.Name = "clickLog";
            this.clickLog.Size = new System.Drawing.Size(396, 295);
            this.clickLog.TabIndex = 8;
            this.clickLog.UseCompatibleStateImageBehavior = false;
            this.clickLog.View = System.Windows.Forms.View.Details;
            // 
            // xCoordinate
            // 
            this.xCoordinate.Text = "X";
            this.xCoordinate.Width = 88;
            // 
            // yCoordinate
            // 
            this.yCoordinate.Text = "Y";
            this.yCoordinate.Width = 87;
            // 
            // timeOccurred
            // 
            this.timeOccurred.Text = "Time";
            this.timeOccurred.Width = 245;
            // 
            // stopRecordingButton
            // 
            this.stopRecordingButton.Location = new System.Drawing.Point(123, 417);
            this.stopRecordingButton.Name = "stopRecordingButton";
            this.stopRecordingButton.Size = new System.Drawing.Size(108, 47);
            this.stopRecordingButton.TabIndex = 9;
            this.stopRecordingButton.Text = "Stop Recording";
            this.stopRecordingButton.UseVisualStyleBackColor = true;
            this.stopRecordingButton.Click += new System.EventHandler(this.stopRecordingButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(259, 73);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(110, 24);
            this.resetButton.TabIndex = 10;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // exportRecordingButton
            // 
            this.exportRecordingButton.Location = new System.Drawing.Point(97, 471);
            this.exportRecordingButton.Name = "exportRecordingButton";
            this.exportRecordingButton.Size = new System.Drawing.Size(116, 31);
            this.exportRecordingButton.TabIndex = 11;
            this.exportRecordingButton.Text = "Export Recording";
            this.exportRecordingButton.UseVisualStyleBackColor = true;
            this.exportRecordingButton.Click += new System.EventHandler(this.exportRecordingButton_Click);
            // 
            // importRecordingButton
            // 
            this.importRecordingButton.Location = new System.Drawing.Point(219, 471);
            this.importRecordingButton.Name = "importRecordingButton";
            this.importRecordingButton.Size = new System.Drawing.Size(116, 31);
            this.importRecordingButton.TabIndex = 12;
            this.importRecordingButton.Text = "Import Recording";
            this.importRecordingButton.UseVisualStyleBackColor = true;
            this.importRecordingButton.Click += new System.EventHandler(this.importRecordingButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // autoClickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 514);
            this.Controls.Add(this.importRecordingButton);
            this.Controls.Add(this.exportRecordingButton);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.stopRecordingButton);
            this.Controls.Add(this.clickLog);
            this.Controls.Add(this.timerText);
            this.Controls.Add(this.timerLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.replayButton);
            this.Controls.Add(this.endButton);
            this.Controls.Add(this.startButton);
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
        private System.Windows.Forms.ListView clickLog;
        private System.Windows.Forms.ColumnHeader xCoordinate;
        private System.Windows.Forms.ColumnHeader yCoordinate;
        private System.Windows.Forms.ColumnHeader timeOccurred;
        private System.Windows.Forms.Button stopRecordingButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button exportRecordingButton;
        private System.Windows.Forms.Button importRecordingButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

