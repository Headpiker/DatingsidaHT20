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

namespace Datingsida.Controllers
{
    public class FriendRequestModelController : Controller
    {
        private readonly DatingDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;

        public FriendRequestModelController(DatingDbContext context, IWebHostEnvironment hostEnvironment, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
        }
        public async IActionResult Index(string profileID)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var ctx = new ApplicationDbContext();            
            
            
            if(currentUser == null)
            {
                return RedirectToAction("Login, ")
            }
        }
    }
}
