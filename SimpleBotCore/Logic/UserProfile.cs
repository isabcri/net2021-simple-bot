using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Logic
{
    public class UserProfile
    {
        public string UserId { get; }
        public string UserName { get; }
        public Uri ServiceUrl { get; }

        public UserProfile(string userId, string username, string serviceUrl)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            this.UserId = userId;
            this.UserName = username;
            this.ServiceUrl = new Uri(serviceUrl);
        }
    }
}
