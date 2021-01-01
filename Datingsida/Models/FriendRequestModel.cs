using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Datingsida.Models
{
    public class FriendRequestModel
    {
        [Key]
        public int FriendRequestID { get; set; }

        [ForeignKey ("Profile")]
        public string ProfileID { get; set; }
        public ProfileModel Id { get; set; }

        [ForeignKey ("Requested")]
        public string RequestID { get; set; }
        public ProfileModel Request { get; set; }

        public List<ProfileModel> Profiles { get; set; }
    }
}
