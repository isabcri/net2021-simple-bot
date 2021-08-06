using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Logic
{
    public class SimpleMessage
    {
        public string UserId { get; }
        public string UserName { get; }
        public string Text { get; }
        public Uri ServiceUrl { get; }

        public SimpleMessage(string id, string username, string text, string serviceUrl)
        {
            this.UserId = id;
            this.UserName = username;
            this.Text = text;
            this.ServiceUrl = new Uri(serviceUrl);
        }
    }
}
