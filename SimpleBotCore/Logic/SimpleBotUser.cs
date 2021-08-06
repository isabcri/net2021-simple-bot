using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Logic
{
    public class SimpleBotUser : ISimpleBotUser
    {
        public string CreateResponse(SimpleMessage message)
        {
            return $"{message.UserName} disse '{message.Text}'";
        }
    }
}
