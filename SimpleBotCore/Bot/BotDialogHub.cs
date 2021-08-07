using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Bot
{
    public class BotDialogHub
    {
        Dictionary<string, BotDialog> _activeBots = new Dictionary<string, BotDialog>();

        // Distribui as mensagens entre os bots
        public void Process(Activity activity)
        {
            string userId = activity.From.Id;

            // Inicia um bot para novos usuarios
            if(!_activeBots.ContainsKey(userId))
            {
                CreateBotDialog(userId, activity);
            }

            // Entrega a mensagem ao bot correspondente
            var bot = _activeBots[userId];

            if( activity.Type == ActivityTypes.Message )
            {
                bot.Accept(activity);
            }
        }

        void CreateBotDialog(string userId, Activity activity)
        {
            var newBot = new BotDialog(activity);
            newBot.Init();

            _activeBots.Add(userId, newBot);
        }
    }
}
