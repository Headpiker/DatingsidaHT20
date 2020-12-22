using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Datingsida.Models
{
    public class ViewAllProfilesModel
    {
        public List<ProfileModel> ProfileModels { get; set; }
        public List<ProfileModel> AllProfileModels { get; set; }
        public List<FriendRequestModel> FriendRequestModels { get; set; }
    }
}
