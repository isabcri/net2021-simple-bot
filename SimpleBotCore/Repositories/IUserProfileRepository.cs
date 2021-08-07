using SimpleBotCore.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Repositories
{
    interface IUserProfileRepository
    {
        SimpleUser TryLoadUser(string userId);

        void Create(SimpleUser user);

        void UpdateName(string userId, string name);
        void IncrementMessageCount(string userId);
    }
}
