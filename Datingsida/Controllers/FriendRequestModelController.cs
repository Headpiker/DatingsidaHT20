using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Datingsida.Models;
using Datingsida.DataAccess;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Datingsida.Data;
using System.Security.Claims;

namespace Datingsida.Controllers
{
    public class FriendRequestModelController : Controller
    {
        private readonly DatingDbContext _context;        
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _application;

        public FriendRequestModelController(DatingDbContext context, UserManager<IdentityUser> userManager, ApplicationDbContext application)
        {
            _context = context;            
            _userManager = userManager;
            _application = application;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddFriend(string userReceiver)
        {            
            var friendrequest = new FriendList
            {                
                Status = false,
                UserReceiver = userReceiver,
                UserSender = _userManager.GetUserId(User)
            };
            _context.Add(friendrequest);
            _context.SaveChanges();            
            return RedirectToAction("Friends");
        }

        public ActionResult AcceptFriendRequest(string UserSender, bool isAccepted)
        {
            var user = _userManager.GetUserId(User); //Hämtar ID för den inloggade användaren
            if (ModelState.IsValid){

                //Hämtar dbc där den som tar emot friendreq är den inloggade användaren och någon annan skickar friendreq
                var request = _context.Friendlists.FirstOrDefault(u => u.UserSender.Equals(UserSender) && u.UserReceiver.Equals(user));
                request.Status = isAccepted;

                if (request.Status){   
                    //Kollar om request har ändrat state, gått från true till false eller vice versa
                    _context.Entry(request).State = EntityState.Modified; 
                }

                else{
                    var removeFriendRequest = _context.Request.Find(request.FriendRequestID);
                    _context.Request.Remove(removeFriendRequest);
                }
                _context.SaveChanges();
                return RedirectToAction("Friends");
            }
            return View("Friends");
        }

        public void GetFriendRequests()
        {
            var user = _userManager.GetUserId(User);
            var count = _context.Friendlists.Count(u => u.UserReceiver.Equals(user) && !u.Status);          

        }

        public ActionResult Friends()
        {
            var friendlist = new FriendListViewModel();
            var user = _userManager.GetUserId(User);
            var requests = _context.Friendlists.Where(u => u.UserReceiver.Equals(user) || u.UserSender.Equals(user)).ToList();

            if (requests != null && requests.Any())
            {
                foreach (var friendrequest in requests)
                {
                    if (friendrequest.Status && friendrequest.UserReceiver.Equals(user))
                    {
                        var dbUser = _context.Profiles.Where(u => u.OwnerId == friendrequest.UserSender).First();
                        friendlist.setFriends(dbUser);
                        List<ProfileModel> lst = new List<ProfileModel>();
                        lst = friendlist.friends;
                        IEnumerable<ProfileModel> enumerableFriendProfiles = lst;

                        return View(enumerableFriendProfiles);
                    }
                    else if (friendrequest.Status && friendrequest.UserSender.Equals(user))
                    {
                        var dbUser = _context.Profiles.Where(u => u.OwnerId == friendrequest.UserReceiver).First();
                        friendlist.setFriends(dbUser);
                        List<ProfileModel> lst = new List<ProfileModel>();
                        lst = friendlist.friends;
                        IEnumerable<ProfileModel> enumerableFriendProfiles = lst;

                        return View(enumerableFriendProfiles);
                    }
                }
                return NotFound();
            }
            return NotFound();

        }      
        
    }
}
