using Microsoft.Bot.Connector;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Bot
{
    public class BotDialog
    {
        readonly static ChannelAccount BotAccount = new ChannelAccount(id: "bot01", name: "Bot");

        readonly BotDialogMessages _messages = new BotDialogMessages();
        readonly string _userId;
        readonly string _conversationid;
        readonly Uri _serviceUrl;

        public BotDialog(Activity activity)
        {
            _userId = activity.From.Id;
            _conversationid = activity.Conversation.Id;
            _serviceUrl = new Uri(activity.ServiceUrl);
        }

        public void Init()
        {
            Task.Run(RunBotConversation);
        }

        public void Accept(Activity activity)
        {
            _messages.Add(activity.Text);
        }

        async Task RunBotConversation()
        {
            while(true)
            {
                try
                {
                    await BotConversation();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        protected virtual async Task BotConversation()
        {
            string nome = null;

            await WriteAsync("Boa noite!");

            await WriteAsync("Qual o seu nome?");

            nome = await ReadAsync();

            await WriteAsync("Bem vindo ao Oraculo, " + nome);

            while(true)
            {
                string line = await ReadAsync();

                await WriteAsync("Você disse: " + line);
            }
        }

        protected Task WriteAsync(string text)
        {
            var connector = new ConnectorClient(_serviceUrl);
            var msg = new Activity(
                type: ActivityTypes.Message,
                text: text,
                replyToId: "",
                conversation: new ConversationAccount() { Id = _conversationid },
                recipient: new ChannelAccount() { Role = "user", Id = _userId, Name = "User" },
                from: BotAccount);
            
            return connector.Conversations.ReplyToActivityAsync(msg);
        }

        protected Task<string> ReadAsync()
        {
            return _messages.ReadAsync();
        }

        class BotDialogMessages
        {
            Queue<string> _messages = new();
            Queue<TaskCompletionSource<string>> _queuedRequests = new();

            public void Add(string message)
            {
                lock(_messages)
                {
                    if (_queuedRequests.Count == 0)
                    {
                        _messages.Enqueue(message);
                    }
                    else
                    {
                        var request = _queuedRequests.Dequeue();
                        request.SetResult(message);
                    }
                
                }
            }

            public Task<string> ReadAsync()
            {
                lock(_messages)
                {
                    if (_messages.Count > 0)
                    {
                        string text = _messages.Dequeue();
                        return Task.FromResult(text);
                    }

                    var tsc = new TaskCompletionSource<string>();

                    _queuedRequests.Enqueue(tsc);

                    return tsc.Task;
                }
            }

            public void Clean()
            {
                lock(_messages)
                {
                    _messages.Clear();
                }
            }
        }
    }
}
