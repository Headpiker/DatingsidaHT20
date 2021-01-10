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
        [Display(Name = "First Name")]
        [Required(ErrorMessage = " Please enter  your first name.")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter your last name.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter your age")]
        [Range(18, 90, ErrorMessage = "Enter number between 18 to 90")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Please click in your gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Please click in your sexuality")]
        public string Sexuality { get; set; }
        public string ImageFilepath { get; set; }
        [NotMapped]
        [DisplayName("Ladda upp bild")]
        [Required(ErrorMessage = "Please choose file to upload profile picture")]
        public IFormFile ImageFile { get; set; }
        [Required(ErrorMessage = "Please fill out presentation field")]
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
