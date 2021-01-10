using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Datingsida.Models
{
    public class ProfileMessageViewModel
    {
        public IEnumerable<ProfileModel> profiles { get; set; }
        public IEnumerable<MessageModel> messages { get; set; }
    }
}
