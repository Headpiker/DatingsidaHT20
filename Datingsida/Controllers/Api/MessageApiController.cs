using Datingsida.DataAccess;
using Datingsida.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Datingsida.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageApiController : ControllerBase
    {
        private readonly DatingDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public MessageApiController(DatingDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;  
        }

        
        [Route("send")]
        [HttpPost]
        [AllowAnonymous]
        public bool Post(MessageModel message)
        {
            try { 
            
           message.FromId = _userManager.GetUserId(User);
           message.DateOfPost = DateTime.Now;

            
            _context.Add(message);
            _context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

    }
}
