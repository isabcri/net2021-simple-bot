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
            if (activity != null)
            {
                await HandleActivityAsync(activity);
            }

            // HTTP 202
            return Accepted();
        }

        // Estabelece comunicacao entre o usuario e o SimpleBotUser
        async Task HandleActivityAsync(Activity activity)
        {
            string userId = activity.From.Id;
            string serviceUrl = activity.ServiceUrl;
            string conversationId = activity.Conversation.Id;
            string text = activity.Text;

            var guest = new SimpleUser(userId, serviceUrl, conversationId);
            var message = new SimpleMessage(userId, text);

            var user = _simpleBot.IdentifyUser(guest);

            string response = null;
            
            switch(activity.Type)
            {
                case ActivityTypes.ConversationUpdate:
                    response = _simpleBot.CreateResponse(user, FirstMessage(user));
                    break;

                case ActivityTypes.Message:
                    response = _simpleBot.CreateResponse(user, message);
                    break;
            }

            if (response != null)
            {
                await user.SendAsync(response);
            }
        }

        SimpleMessage FirstMessage(SimpleUser user)
        {
            return new SimpleMessage(user.Id, "<CONVERSATION STARTED>");
        }
    }
}
