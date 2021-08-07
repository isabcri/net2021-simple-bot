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

        public SimpleUser IdentifyUser(SimpleUser guest)
        {
            var registeredUser = _userProfile.LoadUser(guest.Id);

            return registeredUser ?? CreateGuestUser(guest);
        }

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
            return $"{user.Name} disse '{message.Text}'";
        }

        SimpleUser CreateGuestUser(SimpleUser guest)
        {
            _userProfile.Create(guest);
            return guest;
        }

    }
}
