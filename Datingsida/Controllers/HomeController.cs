using Datingsida.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Datingsida.Data;

namespace Datingsida.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ApplicationDbContext context;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var userID = User.Identity.GetType().ToString();
            var selectProfiles = context.ProfileModel.Where(x => x.Id.ToString() != userID).OrderBy(x => Guid.NewGuid()).ToList();
            var viewProfiles = new ViewAllProfilesModel
            {
                ProfileModels = selectProfiles,
            };
            viewProfiles.ProfileModels.RemoveAll(x => x.IsActive == false);
            
            return View(viewProfiles);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
