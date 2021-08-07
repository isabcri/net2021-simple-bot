using SimpleBotCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Logic
{
    public class SimpleBotUser : ISimpleBotUser
    {
        const string MENSAGEM_BEMVINDO = "Ola User! Qual é o seu nome?";

        IUserProfileRepository _userProfile;
        IMessageRepository _messageHistory;

        public SimpleBotUser(IUserProfileRepository userProfile, IMessageRepository messages)
        {
            _userProfile = userProfile;
            _messageHistory = messages;
        }

        public string CreateResponse(SimpleUser user, SimpleMessage message)
        {
            int messageCount = _messageHistory.GetMessageCount(user.Id);

            _messageHistory.LogMessage(message);

            if(messageCount == 0 )
            {
                return MENSAGEM_BEMVINDO;
            }

            if(messageCount == 1)
            {
                string name = message.Text;

                _userProfile.UpdateName(user.Id, name);

                return $"Olá {name}!";
            }

            // Próximas mensagens
            if ( message.Text.EndsWith("?") )
            {
                return $"{user.Name} PERGUNTOU '{message.Text}'";
            }
            else
            {
                return $"{user.Name} disse '{message.Text}'";
            }
        }

        public SimpleUser IdentifyUser(SimpleUser guest)
        {
            var registeredUser = _userProfile.TryLoadUser(guest.Id);

            if (registeredUser == null)
            {
                registeredUser = CreateGuestUser(guest);
            }

            return registeredUser;
        }

        SimpleUser CreateGuestUser(SimpleUser guest)
        {
            _userProfile.Create(guest);
            return guest;
        }

    }
}
