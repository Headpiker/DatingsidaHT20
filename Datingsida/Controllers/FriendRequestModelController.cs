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

        public FriendRequestModelController(DatingDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;            
            _userManager = userManager;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddFriend(string userReceiver)
        {
            ProfileModel user = new ProfileModel();
            ProfileModel user1 = new ProfileModel();
            var friendrequest = new FriendList
            {                
                Status = false,
                UserReceiver = userReceiver,
                UserSender = _userManager.GetUserId(User)
            };

            _context.Add(friendrequest);
            _context.SaveChanges();
            return RedirectToAction("FriendRequest");
        }

        public ActionResult AcceptFriendRequest(string UserSender, bool isAccepted)
        {
            var user = _userManager.GetUserId(User); //Hämtar ID för den inloggade användaren
            if (ModelState.IsValid)
            {
                //Hämtar dbc där den som tar emot friendreq är den inloggade användaren och någon annan skickar friendreq
                var request = _context.Friendlists.FirstOrDefault(u => u.UserSender.Equals(UserSender) && u.UserReceiver.Equals(user));
                request.Status = isAccepted;

                if (request.Status)
                {   
                    //Kollar om request har ändrat state, gått från true till false eller vice versa
                    _context.Entry(request).State = EntityState.Modified; 
                }
                else
                {
                    var removeFriendRequest = _context.Request.Find(request.FriendRequestID);
                    _context.Request.Remove(removeFriendRequest);
                }
                _context.SaveChanges();
                return RedirectToAction("Friends");
            }
            return View();
        }

        public ActionResult GetFriendRequests()
        {
            var user = _userManager.GetUserId(User);
            var count = _context.Friendlists.Count(u => u.UserReceiver == user && !u.Status);
            return PartialView(count);

        }

        public ActionResult Friends()
        {
            var friendlist = new FriendListViewModel();
            var user = _userManager.GetUserId(User);
            ProfileModel viewmodel = new ProfileModel();

            var requests = _context.Friendlists.Where(u => u.UserReceiver.Equals(user) || u.UserSender.Equals(user)).ToList();

            if (requests != null && requests.Any())
            {
                foreach(var sök in requests)
                {
                    if (sök.Status && sök.UserReceiver.Equals(user))
                    {
                        var dbUser = _userManager.Users.Where(u => u.Id == sök.UserSender).First();
                        var datingdbuser = _context.Profiles.Where(u => u.Id == int.Parse(sök.UserSender)).First();
                        sök.UserSender = viewmodel.Id.ToString();
                        viewmodel.FirstName = datingdbuser.FirstName;
                        viewmodel.Age = datingdbuser.Age;
                        viewmodel.Presentation = datingdbuser.Presentation;
                        viewmodel.Gender = datingdbuser.Gender;
                        friendlist.setFriends(viewmodel);
                        friendlist.Status = sök.Status;                      
                        
                    }
                    else if (sök.Status && sök.UserSender.Equals(user))
                    {
                        var dbUser = _userManager.Users.Where(u => u.Id == sök.UserReceiver).First();
                        var datingdbuser = _context.Profiles.Where(u => u.Id == int.Parse(sök.UserReceiver)).First();
                        sök.UserReceiver = viewmodel.Id.ToString();
                        viewmodel.FirstName = datingdbuser.FirstName;
                        viewmodel.Age = datingdbuser.Age;
                        viewmodel.Presentation = datingdbuser.Presentation;
                        viewmodel.Gender = datingdbuser.Gender;
                        friendlist.setFriends(viewmodel);
                        friendlist.Status = sök.Status;
                    }
                }
                return View(friendlist);
            }
            return View(friendlist);
        }
        
    }
}
