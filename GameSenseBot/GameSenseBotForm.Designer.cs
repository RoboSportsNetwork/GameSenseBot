namespace GameSenseBot
{
    partial class GameSenseBotForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.twitchChannelBox = new System.Windows.Forms.TextBox();
            this.startStopButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.acceptQuestionsBox = new System.Windows.Forms.CheckBox();
            this.sendApologyCheckBox = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Twitch Channel";
            // 
            // twitchChannelBox
            // 
            this.twitchChannelBox.Location = new System.Drawing.Point(99, 13);
            this.twitchChannelBox.Name = "twitchChannelBox";
            this.twitchChannelBox.Size = new System.Drawing.Size(227, 20);
            this.twitchChannelBox.TabIndex = 1;
            // 
            // startStopButton
            // 
            this.startStopButton.Location = new System.Drawing.Point(12, 39);
            this.startStopButton.Name = "startStopButton";
            this.startStopButton.Size = new System.Drawing.Size(314, 38);
            this.startStopButton.TabIndex = 2;
            this.startStopButton.Text = "START";
            this.startStopButton.UseVisualStyleBackColor = true;
            this.startStopButton.Click += new System.EventHandler(this.startStopButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 116);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(338, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel1
            // 
            this.statusLabel1.Name = "statusLabel1";
            this.statusLabel1.Size = new System.Drawing.Size(42, 17);
            this.statusLabel1.Text = "Status:";
            // 
            // acceptQuestionsBox
            // 
            this.acceptQuestionsBox.AutoSize = true;
            this.acceptQuestionsBox.Location = new System.Drawing.Point(15, 83);
            this.acceptQuestionsBox.Name = "acceptQuestionsBox";
            this.acceptQuestionsBox.Size = new System.Drawing.Size(110, 17);
            this.acceptQuestionsBox.TabIndex = 4;
            this.acceptQuestionsBox.Text = "Accept Questions";
            this.acceptQuestionsBox.UseVisualStyleBackColor = true;
            this.acceptQuestionsBox.CheckedChanged += new System.EventHandler(this.acceptQuestionsBox_CheckedChanged);
            // 
            // sendApologyCheckBox
            // 
            this.sendApologyCheckBox.AutoSize = true;
            this.sendApologyCheckBox.Location = new System.Drawing.Point(131, 83);
            this.sendApologyCheckBox.Name = "sendApologyCheckBox";
            this.sendApologyCheckBox.Size = new System.Drawing.Size(92, 17);
            this.sendApologyCheckBox.TabIndex = 5;
            this.sendApologyCheckBox.Text = "Send Apology";
            this.sendApologyCheckBox.UseVisualStyleBackColor = true;
            this.sendApologyCheckBox.CheckedChanged += new System.EventHandler(this.sendApologyCheckBox_CheckedChanged);
            // 
            // GameSenseBotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 138);
            this.Controls.Add(this.sendApologyCheckBox);
            this.Controls.Add(this.acceptQuestionsBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.startStopButton);
            this.Controls.Add(this.twitchChannelBox);
            this.Controls.Add(this.label1);
            this.Name = "GameSenseBotForm";
            this.Text = "GameSenseBot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameSenseBotForm_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox twitchChannelBox;
        private System.Windows.Forms.Button startStopButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel1;
        private System.Windows.Forms.CheckBox acceptQuestionsBox;
        private System.Windows.Forms.CheckBox sendApologyCheckBox;
    }
}