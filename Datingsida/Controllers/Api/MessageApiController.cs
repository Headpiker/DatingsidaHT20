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

        //// GET: api/<MessageApiController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<MessageApiController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<MessageApiController>
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

        // PUT api/<MessageApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MessageApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
