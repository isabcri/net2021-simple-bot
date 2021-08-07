using Microsoft.Bot.Connector;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Logic
{
    public static class SimpleUserExtensions
    {
        const string MESSAGE_ACTIVITY = "message";
        readonly static ChannelAccount BotAccount = new ChannelAccount(id: "bot1", name: "Bot");

        public static Task SendAsync(this SimpleUser user, string text)
        {
            var connector = new ConnectorClient(new Uri(user.ServiceUrl));
            var msg = new Activity(
                type: MESSAGE_ACTIVITY,
                text: text,
                replyToId: "",
                conversation: new ConversationAccount() { Id = user.LastConversation },
                recipient: new ChannelAccount() { Role = "user", Id = user.Id, Name = user.Name },
                from: BotAccount);

            return connector.Conversations.ReplyToActivityAsync(msg);
        }
    }
}
