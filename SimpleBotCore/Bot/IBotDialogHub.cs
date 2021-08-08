using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Bot
{
    public interface IBotDialogHub
    {
        Task ProcessAsync(Activity activity);
    }
}
