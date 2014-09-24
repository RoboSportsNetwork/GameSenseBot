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
    }
}
