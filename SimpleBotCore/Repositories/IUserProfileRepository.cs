using SimpleBotCore.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Repositories
{
    interface IUserProfileRepository
    {
        SimpleUser LoadUser(string userId);

        void Create(SimpleUser user);

        void Update(SimpleUser user);
    }
}
