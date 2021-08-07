using SimpleBotCore.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Repositories
{
    public interface IMessageRepository
    {
        void LogMessage(SimpleMessage message);
        int GetMessageCount(string userId);
        IEnumerable<string> GetMessageHistory(string userId);
    }
}
