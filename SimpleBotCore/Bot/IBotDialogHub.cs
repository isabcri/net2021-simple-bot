using System.Threading.Tasks;
using Microsoft.Bot.Schema;

namespace SimpleBotCore.Bot
{
    public interface IBotDialogHub
    {
        Task ProcessAsync(Activity activity);     
    }
}
