using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Datingsida.Controllers
{
    public class FriendRequestModelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
