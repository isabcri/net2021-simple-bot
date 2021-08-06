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
        private ISimpleBotUser _simpleBot;

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

            SimpleMessage message = new SimpleMessage(userFromId, userFromName, text, serviceUrl);
            string response = _simpleBot.CreateResponse(message);

            if( response != null )
            {
                await ReplyUserAsync(activity, message, response);
            }
        }

        // Responde mensagens usando o Bot Framework Connector
        static async Task ReplyUserAsync(Activity message, SimpleMessage input, string text)
        {
            var connector = new ConnectorClient(input.ServiceUrl);
            var reply = message.CreateReply(text);
            var msg = new Activity(type: "message", text: text, 
                replyToId: "1", 
                conversation: new ConversationAccount() { Id = message.Conversation.Id },
                recipient: message.From,
                from: new ChannelAccount(id: "bot1", name: "Bot"));            

            await connector.Conversations.ReplyToActivityAsync(msg);            
        }
    }
}
