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
using Datingsida.Data;

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

        public async Task<IActionResult> Index()
        {
            return View(await _context.Profiles.ToListAsync());
            
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
