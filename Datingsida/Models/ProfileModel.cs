using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace Datingsida.Models
{
    

    public class ProfileModel
    {
        [Key]
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
        [DisplayName("Ladda upp bild")]
        public IFormFile ImageFile { get; set; }
        [Required]
        public string Presentation { get; set; }
        public bool IsActive { get; set; }

        //public List<ProfileModel> friends = new List<ProfileModel>();
        //public List<ProfileModel> Userinfo { get; set; }
                
        public ProfileModel()
        {
        }        

    }

    public class FriendListViewModel
    {
        public bool Status { get; set; }
        public List<ProfileModel> friends = new List<ProfileModel>();
        public void setFriends(ProfileModel user)
        {
            friends.Add(user);
        }
    }
}
