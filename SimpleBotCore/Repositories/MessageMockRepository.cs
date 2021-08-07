using SimpleBotCore.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Repositories
{
    public class MessageMockRepository : IMessageRepository
    {
        Dictionary<string, List<string>> _logs = new Dictionary<string, List<string>>();

        public void LogMessage(SimpleMessage message)
        {
            string userId = message.UserId;
            string text = message.Text;

            if ( !_logs.ContainsKey(userId) )
            {
                _logs[userId] = new List<string>();
            }

            _logs[userId].Add(text);
        }

        public int GetMessageCount(string userId)
        {
            if (!_logs.ContainsKey(userId))
            {
                return 0;
            }

            return _logs[userId].Count;
        }
    }
}
