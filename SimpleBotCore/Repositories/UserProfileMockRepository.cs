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

        public SimpleUser TryLoadUser(string userId)
        {
            if( Exists(userId) )
            {
                return GetUser(userId);
            }

            return null;
        }

        public void Create(SimpleUser user)
        {
            if ( Exists(user.Id) )
                throw new InvalidOperationException("Usuário ja existente");

            SaveUser(user);
        }

        public void UpdateName(string userId, string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (!Exists(userId))
                throw new InvalidOperationException("Usuário não existe");

            var user = GetUser(userId);

            user.Name = name;

            SaveUser(user);
        }

        public void IncrementMessageCount(string userId)
        {
            if (!Exists(userId))
                throw new InvalidOperationException("Usuário não existe");

            var user = GetUser(userId);

            user.MessageCount = user.MessageCount + 1;
            
            SaveUser(user);
        }

        private bool Exists(string userId)
        {
            return _users.ContainsKey(userId);
        }

        private SimpleUser GetUser(string userId)
        {
            return Clone(_users[userId]);
        }

        private void SaveUser(SimpleUser user)
        {
            _users[user.Id] = Clone(user);
        }

        public SimpleUser Clone(SimpleUser user)
        {
            var newUser = new SimpleUser(user.Id, user.ServiceUrl, user.LastConversation);
            newUser.Name = user.Name;
            newUser.MessageCount = user.MessageCount;

            return newUser;
        }
    }
}

