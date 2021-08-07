using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Logic
{
    public class SimpleUser
    {
        public string Id { get; }
        public string Name { get; set; }
        public string LastConversation { get; }

        public string ServiceUrl { get; }
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

        public SimpleUser Clone()
        {
            var user = new SimpleUser(this.Id, this.ServiceUrl, this.LastConversation);

            return user;
        }
    }
}
