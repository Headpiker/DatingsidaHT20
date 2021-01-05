using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Datingsida.Models
{
    public class FriendList
    {
        [Key]
        public int FriendRequestID { get; set; }
        public string UserReceiver { get; set; }
        public string UserSender { get; set; }
        public bool Status { get; set; }

        
    }
}
