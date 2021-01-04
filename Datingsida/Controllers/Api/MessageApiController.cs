using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Datingsida.DataAccess;
using Datingsida.Models;

namespace Datingsida.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageApiController : ControllerBase
    {
        private readonly DatingDbContext _context;

        public MessageApiController(DatingDbContext context)
        {
            _context = context;
        }

        // GET: api/MessageApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageModel>>> GetMessageModel()
        {
            return await _context.Messages.ToListAsync();
        }

        // GET: api/MessageApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageModel>> GetMessageModel(int id)
        {
            var messageModel = await _context.Messages.FindAsync(id);

            if (messageModel == null)
            {
                return NotFound();
            }

            return messageModel;
        }

        // PUT: api/MessageApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessageModel(int id, MessageModel messageModel)
        {
            if (id != messageModel.MessageId)
            {
                return BadRequest();
            }

            _context.Entry(messageModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MessageApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MessageModel>> PostMessageModel(MessageModel messageModel)
        {
            _context.Messages.Add(messageModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMessageModel", new { id = messageModel.MessageId }, messageModel);
        }

        // DELETE: api/MessageApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessageModel(int id)
        {
            var messageModel = await _context.Messages.FindAsync(id);
            if (messageModel == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(messageModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessageModelExists(int id)
        {
            return _context.Messages.Any(e => e.MessageId == id);
        }
    }
}
