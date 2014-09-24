using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meebey.SmartIrc4net;
using System.Threading;

namespace GameSenseBot
{
    class GSTwitchClient
    {
        private  IrcClient irc = new IrcClient();
        private  string server = "irc.twitch.tv";
        private  int port = 6667;
        private string channel = "#frcgamesense";
        private string oauth = "oauth:3m63uo712mbw62271j7y0czsu549hri";
        private List<string[]> questions = new List<string[]>();
        private GSBotCommands messageCommands = new GSBotCommands();
        private GSBotTbaCommunicator tba = new GSBotTbaCommunicator();
        public static Thread twitchThread;
        private string status;

        public string Status
        {
            get
            {
                return status;
            }
        }
        
        public List<string[]> QuestionsList
        {
            get
            {
                return questions;
            }
            set
            {
                //shouldn't set
            }
        }

        public GSTwitchClient(string myChannel)
        {
            this.channel = myChannel;
            irc.OnConnected += new EventHandler(OnConnected);
            irc.OnConnecting += new EventHandler(OnConnecting);
            irc.OnDisconnected += new EventHandler(OnDisconnected);
            irc.OnChannelMessage += new IrcEventHandler(OnChannelMessage);

            irc.AutoRetry = true;
            irc.AutoRetryDelay = 10;
            irc.AutoReconnect = true;            
        }

        public void Connect()
        {
            try
            {
                irc.Connect(server, port);
            }
            catch (Exception e)
            {
                status = "Failed to connect: " + e.Message;
                Console.WriteLine("Failed to connect:n" + e.Message);
                Console.ReadKey();
            }
        }

        void OnPing(object sender, PingEventArgs e)
        {
            Console.WriteLine("Responded to ping at {0}", DateTime.Now.ToShortTimeString());
        }

        void OnConnected(object sender, EventArgs e)
        {
            Console.WriteLine("Connected.");
            status = "Connected";
            irc.Login("GameSenseBot", "GameSense Bot", 0, "GameSenseBot", oauth);
            irc.RfcJoin(channel);
        }

        public void Listen()
        {
            twitchThread = new Thread(new ThreadStart(irc.Listen));
            twitchThread.Start();
        }

        public void Stop()
        {
            irc.Disconnect();
            status = "Disconnected";
            if (twitchThread != null)
            {
                twitchThread.Abort();
            }
        }

        void OnConnecting(object sender, EventArgs e)
        {
            status = "Connecting";
            Console.WriteLine("Connecting to {0}", server);
        }
        
        void OnDisconnected(object sender, EventArgs e)
        {
            Console.WriteLine("Disconnected");
            status = "Disconnected";
        }

        void SendChannelMessage(string message)
        {
            irc.SendMessage(SendType.Message, channel, message, Priority.BelowMedium);
        }

        /// <summary>
        /// Recieve a message, process it, and handle it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnChannelMessage(object sender, IrcEventArgs e)
        {
            Console.WriteLine(e.Data.Type + ".");
            Console.WriteLine("(" + e.Data.Channel + ") <" + e.Data.Nick + "> " + e.Data.Message);

            //pull the message out of the object for ease of use
            string rawMessage = e.Data.Message;


            if (rawMessage.StartsWith("!"))
            {
                string[] messageArray;
                if (rawMessage.Contains(' '))
                {
                    messageArray = rawMessage.Split(' ');
                }
                else
                {
                    messageArray = new string[1] { rawMessage };
                }

                //check to see if the first item in the array matches one of a predefined set of commands
                if (messageArray[0] == messageCommands.Question.Command || messageArray[0] == messageCommands.QuestionShort.Command)
                {
                    //if its a question, add the question to the questions list.
                    parseAndAddQuestion(messageArray, e.Data.Nick);
                }
                else if (messageArray[0] == messageCommands.Tba.Command)
                {
                    if (isNum(messageArray[1]))
                    {
                        //blindly add the requested number to the end of the TBA team url.
                        //TODO: check to see if it's an actual team to prevent linking to inexistant pages.
                        SendChannelMessage("Here's FIRST Team " + messageArray[1] + "'s page on TBA: www.thebluealliance.com/team/" + messageArray[1]);
                    }
                }
                else if (messageArray[0] == messageCommands.Help.Command)
                {
                    //if they asked for help, build a response from the list of predefined commands
                    //TODO: eventually link to a wiki page as this is already getting pretty cumbersome
                    StringBuilder sb = new StringBuilder();
                    sb.Append("The commands I currently understand are: ");
                    foreach (GSBotCommand cmd in messageCommands.CommandsList)
                    {
                        sb.Append("\"" + cmd.Command + "\" (" + cmd.Description + "), ");
                    }
                    string msg = sb.ToString();
                    SendChannelMessage(msg.Substring(0, msg.Length - 2));
                }
                else if (messageArray[0] == messageCommands.TeamName.Command)
                {
                    //Get the team from TBA and return its name.
                    if (isNum(messageArray[1]))
                    {
                        TBATeam team = tba.getTeam("frc" + messageArray[1]);
                        SendChannelMessage("FIRST Team " + messageArray[1] + "'s nickname is: " + team.nickname);
                    }
                }
                else if (messageArray[0] == messageCommands.TeamRecord.Command)
                {

                    string team = messageArray[1];
                    string eventKey = null;
                    string year = null;
                    List<TBAMatch> matches;
                    
                    if (isNum(team))
                    {
                        //if they provided an event or year, handle that
                        if (messageArray.Count() > 2)
                        {
                            //handle a request for a full record of a specific year
                            if (isNum(messageArray[2]))
                            {
                                year = messageArray[2];
                                matches = tba.getAllTeamMatches("frc" + team, year);
                                SendChannelMessage(team + " was " + tba.getTeamRecordAtEvent("frc" + team, matches) + " in " + year + ".");
                            }
                            //handle a request for a specific event.
                            else
                            {
                                eventKey = messageArray[2];
                                matches = tba.getTeamEventMatches("frc" + team, eventKey);
                                SendChannelMessage(team + " was " + tba.getTeamRecordAtEvent("frc" + team, matches) + " at " + eventKey + ".");
                            }
                        }
                        //handle a request for all matches this year.
                        else
                        {
                            matches = tba.getAllTeamMatches("frc" + team);
                            SendChannelMessage(team + " was " + tba.getTeamRecordAtEvent("frc" + team, matches) + " this year.");
                        }
                    }
                }
                else if (messageArray[0] == messageCommands.TeamEvents.Command)
                {
                    string team = messageArray[1];
                    string year = null;
                    List<TBAEvent> events = new List<TBAEvent>();
                    if (isNum(team))
                    {
                        //handle a request for a specific year
                        if (messageArray.Count() > 2)
                        {
                            year = messageArray[2];
                            if (isNum(year))
                            {
                                events = tba.getTeamEvents("frc" + team, year);
                            }
                        }
                        else
                        {
                            events = tba.getTeamEvents("frc" + team);
                        }

                        StringBuilder sb = new StringBuilder();
                        foreach (TBAEvent evt in events)
                        {
                            //smartly construct the string so it's readable
                            if (events.Last<TBAEvent>() == evt)
                            {
                                sb.Append("and " + evt.name);
                            }
                            else
                            {
                                sb.Append(evt.name + ", ");
                            }
                        }
                        SendChannelMessage(team + " is registered for " + sb.ToString() + " in " + year + ".");
                    }
                }
                else
                {
                    SendChannelMessage("I'm sorry " + e.Data.Nick + ", I didn't understand that tag.  Type \"!help\" for a list of commands I can understand.");
                }
            }
        }

        /// <summary>
        /// Adds a question to the questions list.
        /// </summary>
        /// <param name="messageArray">The array of strings representing a question</param>
        /// <param name="nick">The nickname of the individual that asked the question</param>
        private void parseAndAddQuestion(string[] messageArray, string nick)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string str in messageArray)
            {
                if (!str.StartsWith("!"))
                {
                    sb.Append(str + " ");
                }
            }

            string parsed = sb.ToString();

            string[] question = { parsed.Substring(0,parsed.Length-1), nick };
            questions.Add(question);
            SendChannelMessage("Thanks, " + nick + ". Your question has been added to the queue!");
        }

        /// <summary>
        /// Determines if a string contains only numerical digits.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>true if a string contains only numerical digits, false otherwise.</returns>
        public bool isNum(string str)
        {
            int teamNum;
            return int.TryParse(str, out teamNum);
        }
    }
}
