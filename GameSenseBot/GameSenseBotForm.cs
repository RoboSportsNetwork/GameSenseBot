using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameSenseBot
{
    public partial class GameSenseBotForm : Form
    {
        private GSTwitchClient twitch;

        public GameSenseBotForm()
        {
            InitializeComponent();
        }

        private void startStopButton_Click(object sender, EventArgs e)
        {
            if (startStopButton.Text == "START")
            {
                if (!twitchChannelBox.Text.StartsWith("#"))
                {
                    twitchChannelBox.Text = "#" + twitchChannelBox.Text;
                }
                twitch = new GSTwitchClient(twitchChannelBox.Text);
                twitch.Connect();
                statusLabel1.Text = "Status: " + twitch.Status;
                twitch.Listen();

                startStopButton.Text = "STOP";
            }
            else
            {
                twitch.Stop();
                statusLabel1.Text = "Status: " + twitch.Status;
                startStopButton.Text = "START";
            }
        }

        private void acceptQuestionsBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.acceptingQuestions = acceptQuestionsBox.Checked;
            Properties.Settings.Default.Save();

            sendApologyCheckBox.Enabled = !acceptQuestionsBox.Checked;
        }

        private void sendApologyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.sendApology = sendApologyCheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void GameSenseBotForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            twitch.Stop();
            statusLabel1.Text = "Status: " + twitch.Status;
        }
    }
}
