using Microsoft.Bot.Schema;
using System.Threading.Tasks;

namespace SimpleBotCore.Bot
{
    public interface IBotDialogHub
    {
        Task ProcessAsync(Activity activity);
        Task ProcessAsync(System.Diagnostics.Activity activity);       
    }
}
