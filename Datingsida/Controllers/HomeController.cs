using Datingsida.DataAccess;
using Datingsida.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Datingsida.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatingDbContext _context;

        public HomeController(DatingDbContext context,ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var myModel = new ProfileMessageViewModel();
            myModel.profiles = await _context.Profiles.ToListAsync();
            myModel.messages = await _context.Messages.ToListAsync();
            return View(myModel);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
