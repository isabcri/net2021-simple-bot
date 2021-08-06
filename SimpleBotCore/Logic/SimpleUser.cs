using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Logic
{
    public class SimpleUser
    {
        public string UserId { get; }
        public string UserName { get; }
        public string LastConversation { get; }

        public Uri ServiceUrl { get; }

        public SimpleUser(string userId, string username, string serviceUrl, string conversationId)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            this.UserId = userId;
            this.UserName = username;
            this.LastConversation = conversationId;
            this.ServiceUrl = new Uri(serviceUrl);
        }
    }
}
