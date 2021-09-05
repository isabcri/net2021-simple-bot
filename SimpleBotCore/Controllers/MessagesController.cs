using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Schema;
using SimpleBotCore.Bot;
using System.Threading.Tasks;

namespace SimpleBotCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        static IBotDialogHub _botHub;

        public MessagesController(IBotDialogHub botHub)
        {
            _botHub = botHub;
        }

        [HttpGet]
        public string Get()
        {
            return "Simple Bot está online";
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Activity activity)
        {
            await _botHub.ProcessAsync(activity);

            return Accepted();
        }
    }
}
