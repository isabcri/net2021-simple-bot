using Microsoft.Bot.Connector;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Bot
{
    public class BotDialog
    {
        readonly static ChannelAccount BotAccount = new ChannelAccount(id: "bot01", name: "Bot");

        readonly string _userId;
        readonly string _conversationid;
        readonly Uri _serviceUrl;

        public BotDialog(Activity activity)
        {
            _userId = activity.From.Id;
            _conversationid = activity.Conversation.Id;
            _serviceUrl = new Uri(activity.ServiceUrl);
        }

        public void Init()
        {
            Task.Run(BotConversation);
        }

        public void Accept(Activity activity)
        {
            Console.WriteLine("accept");
        }

        public async void BotConversation()
        {
            await WriteAsync("Boa noite!");

            for(int i=0; i<10;i++)
            {
                await WriteAsync(i.ToString());
                await Task.Delay(1000);
            }
        }

        protected Task WriteAsync(string text)
        {
            var connector = new ConnectorClient(_serviceUrl);
            var msg = new Activity(
                type: ActivityTypes.Message,
                text: text,
                replyToId: "",
                conversation: new ConversationAccount() { Id = _conversationid },
                recipient: new ChannelAccount() { Role = "user", Id = _userId, Name = "User" },
                from: BotAccount);
            
            return connector.Conversations.ReplyToActivityAsync(msg);
        }
    }
}
