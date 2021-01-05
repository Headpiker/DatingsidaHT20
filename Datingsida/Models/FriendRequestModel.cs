using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Datingsida.Models
{
    public class UsersIndexViewModel
    {
        public List<FriendRequestModel> Userinfo { get; set; }
    }
    public class FriendRequestModel
    {

        public int Id { get; set; }
        //Id från AspNetUser
        [ForeignKey("OwnerId")]
        public string OwnerId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Sexuality { get; set; }
        public string ImageFilepath { get; set; }  
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [Required]
        public string Presentation { get; set; }
        public bool IsActive { get; set; }

        public List<FriendRequestModel> friends = new List<FriendRequestModel>();

        public void setFriends(FriendRequestModel user)
        {
            friends.Add(user);
        }

    }

    
}
