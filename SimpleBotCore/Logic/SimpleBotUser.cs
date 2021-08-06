using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Logic
{
    public class SimpleBotUser : ISimpleBotUser
    {
        public string CreateResponse(UserProfile user, SimpleMessage message)
        {
            return $"{user.UserName} disse '{message.Text}'";
        }
    }
}
