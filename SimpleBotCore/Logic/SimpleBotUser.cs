using SimpleBotCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Logic
{
    public class SimpleBotUser : ISimpleBotUser
    {
        IUserProfileRepository _userProfile = new UserProfileMockRepository();
        
        public string CreateResponse(SimpleUser user, SimpleMessage message)
        {
            _userProfile.IncrementMessageCount(user.Id);

            // Primeira mensagem
            if ( user.MessageCount == 0 || message == null )
            {
                return "Ola User! Qual é o seu nome?";
            }

            // Segunda mensagem
            if ( user.MessageCount == 1 )
            {
                string name = message.Text;

                _userProfile.UpdateName(user.Id, name);

                return $"Olá {name}!";
            }

            // Próximas mensagens
            if (message.Text.EndsWith("?"))
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
