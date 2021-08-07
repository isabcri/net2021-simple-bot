using SimpleBotCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Logic
{
    public class SimpleBotUser : ISimpleBotUser
    {
        IUserProfileRepository _userProfile;

        public SimpleBotUser(IUserProfileRepository userProfile)
        {
            _userProfile = userProfile;
        }

        public string CreateResponse(SimpleUser user, SimpleMessage message)
        {
            _userProfile.IncrementMessageCount(user.Id);

            int messageCount = user.MessageCount;

            // Primeira mensagem
            if ( messageCount == 0 )
            {
                return "Ola User! Qual é o seu nome?";
            }

            // Segunda mensagem
            if ( messageCount == 1 )
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
