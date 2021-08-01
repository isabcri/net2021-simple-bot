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
            string text = activity.Text;
            string userFromId = activity.From.Id;
            string userFromName = activity.From.Name;

            SimpleMessage message = new SimpleMessage(userFromId, userFromName, text);
            string response = _simpleBot.CreateResponse(message);

            await ReplyUserAsync(activity, response);
        }

        // Responde mensagens usando o Bot Framework Connector
        static async Task ReplyUserAsync(Activity message, string text)
        {
            var connector = new ConnectorClient(new Uri(message.ServiceUrl));
            var reply = message.CreateReply(text);

            await connector.Conversations.ReplyToActivityAsync(reply);
        }
    }
}
