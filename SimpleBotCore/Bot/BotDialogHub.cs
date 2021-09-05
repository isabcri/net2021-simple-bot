using Microsoft.AspNetCore.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Bot
{
    public class BotDialogHub : IBotDialogHub
    {
        Dictionary<string, BotDialog> _activeBots = new Dictionary<string, BotDialog>();
        IServiceProvider _serviceProvider;

        public BotDialogHub(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public Task ProcessAsync(Activity activity)
        {
            string userId = activity.From.Id;

            if (!_activeBots.ContainsKey(userId))
            {
                CreateBotDialog(userId, activity);
            }
            else
            {
                var bot = _activeBots[userId];

                if (activity.Type == ActivityTypes.Message)
                {
                    bot.Accept(activity);
                }
            }

            return Task.CompletedTask;
        }
        void CreateBotDialog(string userId, Activity activity)
        {
            var newBot = _serviceProvider.GetService<BotDialog>();

            newBot.Init(activity);

            _activeBots.Add(userId, newBot);
        }
    }
}
