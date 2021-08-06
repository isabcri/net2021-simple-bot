using SimpleBotCore.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Repositories
{
    public class UserProfileMockRepository : IUserProfileRepository
    {
        Dictionary<string, SimpleUser> _users = new Dictionary<string, SimpleUser>();

        public UserProfileMockRepository()
        {
        }

        public SimpleUser LoadUser(string userId)
        {
            if( _users.ContainsKey(userId) )
            {
                return _users[userId];
            }

            return null;
        }

        public void Create(SimpleUser user)
        {
            if (_users.ContainsKey(user.UserId))
                throw new InvalidOperationException("Usuario ja existente");

            _users[user.UserId] = user;
        }

        public void Update(SimpleUser user)
        {
            _users[user.UserId] = user;
        }
    }
}
