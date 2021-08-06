using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Logic
{
    public class SimpleMessage
    {
        public string UserId { get; }
        public string Conversation { get; }
        public string Text { get; }

        public SimpleMessage(string userId, string conversationId, string text)
        {
            this.UserId = userId;
            this.Conversation = conversationId;
            this.Text = text;
        }
    }
}
