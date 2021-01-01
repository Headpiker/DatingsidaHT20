using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Datingsida.Models
{
    public class ChosenProfileView
    {
        public ProfileModel Profile { get; set; }
        public FriendRequestModel ReceivedRequests { get; set; }
        public FriendRequestModel SentRequests { get; set; }

    }
}
