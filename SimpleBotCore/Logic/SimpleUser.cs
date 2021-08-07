using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Logic
{
    public class SimpleUser
    {
        public string Id { get; }
        public string LastConversation { get; }
        public string ServiceUrl { get; }
        public string Name { get; set; }
        public int MessageCount { get; set; }

        public SimpleUser(string userId, string serviceUrl, string conversationId)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            this.Id = userId;
            this.LastConversation = conversationId;
            this.ServiceUrl = serviceUrl;
            this.Name = null;
            this.MessageCount = 0;
        }
    }
}
