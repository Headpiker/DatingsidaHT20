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
            //ProfileModel user = new ProfileModel();
            //ProfileModel user1 = new ProfileModel();
            //ProfileModel user = new ProfileModel();
            //ProfileModel user1 = new ProfileModel();
            var friendrequest = new FriendList
            {

                Status = false,
            _context.SaveChanges();
            return RedirectToAction("Friends");
            };
            _context.Add(friendrequest);
            _context.SaveChanges();
            return RedirectToAction("GetFriendRequests");
        }
            if (ModelState.IsValid)
            {
                if (isAccepted != null)
                {
                    //Hämtar dbc där den som tar emot friendreq är den inloggade användaren och någon annan skickar friendreq
                    var request = _context.Friendlists.FirstOrDefault(u => u.UserSender.Equals(UserSender) && u.UserReceiver.Equals(user));
                    request.Status = (bool)isAccepted;
            {
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
            }
            return View();
                _context.SaveChanges();
                return RedirectToAction("Friends");
        public string GetFriendRequests()
            return View();
        }
            var count = _context.Friendlists.Count(u => u.UserReceiver.Equals(user) && !u.Status);
            return count.ToString();
            var user = _userManager.GetUserId(User);
            var count = _context.Friendlists.Count(u => u.UserReceiver.Equals(user) && !u.Status);
            return RedirectToAction("Friends");

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
                        List<ProfileModel> lst = new List<ProfileModel>();
                        lst = friendlist.friends;
                        IEnumerable<ProfileModel> enumerableFriendProfiles = lst;

                        List<ProfileModel> lst = new List<ProfileModel>();
                        lst = friendlist.friends;
                        IEnumerable<ProfileModel> enumerableFriendProfiles = lst;

            TempData["noFriend"] =  ViewData["noFriends"] = true;
            TempData["noFriendRequest"] = ViewData["noFriendRequests"] = false;
  
            return RedirectToAction("NoFriendOrFriendRequest");
        }

        public ActionResult Requests()
        {
            var friendRequests = new List<ProfileModel>();
            IEnumerable<ProfileModel> enumerableFriendRequests = null;

            var user = _userManager.GetUserId(User);
            var requests = _context.Friendlists.Where(u => u.UserReceiver == user).ToList();

            if (requests != null && requests.Any())
            {
                    foreach (var request in requests)
                {
                    if (!request.Status)
                    {
                        var senderProfile = _context.Profiles.Where(u => u.OwnerId == request.UserSender).First();
                        friendRequests.Add(senderProfile);
                        enumerableFriendRequests = friendRequests;
                    }
                }
                if (enumerableFriendRequests != null)
                {
                    return View(enumerableFriendRequests);
                }
                TempData["noFriend"] = ViewData["noFriends"] = false;
                TempData["noFriendRequest"] = ViewData["noFriendRequests"] = true;
                return RedirectToAction("NoFriendOrFriendRequest");

            }
            TempData["noFriend"] = ViewData["noFriends"] = false;
            TempData["noFriendRequest"] = ViewData["noFriendRequests"] = true;
            return RedirectToAction("NoFriendOrFriendRequest");
        }
        public ActionResult NoFriendOrFriendRequest()
        {
            ViewBag.noFriends = TempData["noFriend"];
            ViewBag.noFriendRequests = TempData["noFriendRequest"];

            return View();
        }
            }
            return View(friendlist);
        }
        
    }
}
