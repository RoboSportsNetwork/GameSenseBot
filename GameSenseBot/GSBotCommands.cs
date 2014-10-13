using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSenseBot
{
    class GSBotCommands
    {
        public GSBotCommand Question = new GSBotCommand("!question", "Used to ask a question");
        public GSBotCommand QuestionShort = new GSBotCommand("!q", "Shorthand for asking a question");
        public GSBotCommand Tba = new GSBotCommand("!tba", "Type a team number after this to link that team's TBA page");
        public GSBotCommand TeamName = new GSBotCommand("!name", "Type a team number after this to get that team's short name");
        public GSBotCommand TeamRecord = new GSBotCommand("!record", "Type a team number and an event code after this to get that team's record at that event.");
        public GSBotCommand TeamEvents = new GSBotCommand("!events", "Type a team number after this to get a list of events that team is registered for this year.");
        public GSBotCommand Help = new GSBotCommand("!help", "Use this to list all understandable commands");

        public List<GSBotCommand> CommandsList{
            get{
                List<GSBotCommand> list = new List<GSBotCommand>();
                if (Properties.Settings.Default.acceptingQuestions)
                {
                    list.Add(Question);
                    list.Add(QuestionShort);
                }
                list.Add(Tba);
                list.Add(TeamName);
                list.Add(TeamRecord);
                list.Add(Help);

                return list;
            }
        }

        public GSBotCommands()
        {
        }
            
    }
}
