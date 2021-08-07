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
            var user = _userProfile.LoadUser(guest.Id);

            if (user == null)
            {
                user = guest;
                _userProfile.Create(guest);
            }

            return user;
        }

        public string CreateResponse(SimpleUser user, SimpleMessage message)
        {
            _userProfile.IncrementMessageCount(user.Id);

            if ( user.MessageCount == 1 || message == null )
            {
                return "Ola User! Qual é o seu nome?";
            }

            if ( user.MessageCount == 2 )
            {
                string name = message.Text;

                _userProfile.UpdateName(user.Id, name);

                return $"Olá {name}!";
            }

            return $"{user.Name} disse '{message.Text}'";
        }
    }
}
