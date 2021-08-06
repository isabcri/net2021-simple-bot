using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Logic
{
    public class SimpleMessage
    {
        public string UserId { get; }
        public string Text { get; }

        public SimpleMessage(string userId, string text)
        {
            this.UserId = userId;
            this.Text = text;
        }
    }
}
