using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Datingsida.Models
{
    public class ViewAllProfilesModel
    {
        public List<ProfileModel> Profiles { get; set; }
        public List<ProfileModel> AllProfiles { get; set; }
        public List<ProfileModel> Requests { get; set; }
    }
}
