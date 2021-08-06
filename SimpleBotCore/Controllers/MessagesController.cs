using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using SimpleBotCore.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        const string MESSAGE_ACTIVITY = "message";
        readonly static ChannelAccount BotAccount = new ChannelAccount(id: "bot1", name: "Bot");
        readonly private ISimpleBotUser _simpleBot;

        public MessagesController(ISimpleBotUser simpleBot)
        {
            _simpleBot = simpleBot;
        }

        [HttpGet]
        public string Get()
        {
            return "Simple Bot está online";
        }

        // POST api/messages
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Activity activity)
        {
            if (activity != null && activity.Type == ActivityTypes.Message)
            {
                await HandleActivityAsync(activity);
            }

            // HTTP 202
            return Accepted();
        }

        // Estabelece comunicacao entre o usuario e o SimpleBotUser
        async Task HandleActivityAsync(Activity activity)
        {
            string serviceUrl = activity.ServiceUrl;
            string userFromId = activity.From.Id;
            string userFromName = activity.From.Name;
            string conversationId = activity.Conversation.Id;
            string text = activity.Text;

            var user = new UserProfile(userFromId, userFromName, serviceUrl);
            var message = new SimpleMessage(userFromId, conversationId, text);

            string response = _simpleBot.CreateResponse(user, message);

            if( response != null )
            {
                await ReplyUserAsync(user, message, response);
            }
        }

        // Responde mensagens usando o Bot Framework Connector
        static async Task ReplyUserAsync(UserProfile user, SimpleMessage input, string text)
        {
            var connector = new ConnectorClient(user.ServiceUrl);
            var msg = new Activity(
                type: MESSAGE_ACTIVITY, 
                text: text, 
                replyToId: "", 
                conversation: new ConversationAccount() { Id = input.Conversation },
                recipient: new ChannelAccount() { Role = "user", Id = user.UserId, Name = user.UserName },
                from: BotAccount);            

            await connector.Conversations.ReplyToActivityAsync(msg);            
        }
    }
}
