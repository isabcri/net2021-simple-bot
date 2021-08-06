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


            return $"{user.Name} disse '{message.Text}'";
        }
    }
}
