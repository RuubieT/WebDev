using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDevAPI.Models;

namespace WebDevAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactformModelsController : ControllerBase
    {
        private readonly ContactformContext _context;

        public ContactformModelsController(ContactformContext context)
        {
            _context = context;
        }

        // GET: api/ContactformModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactformModel>>> GetContactforms()
        {
            return await _context.Contactforms.ToListAsync();
        }

        // GET: api/ContactformModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactformModel>> GetContactformModel(int id)
        {
            var contactformModel = await _context.Contactforms.FindAsync(id);

            if (contactformModel == null)
            {
                return NotFound();
            }

            return contactformModel;
        }

        // PUT: api/ContactformModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactformModel(int id, ContactformModel contactformModel)
        {
            if (id != contactformModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(contactformModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactformModelExists(id))
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

        // POST: api/ContactformModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContactformModel>> PostContactformModel(ContactformModel contactformModel)
        {
            if (contactformModel == null) return NotFound();
            if (contactformModel.Description.Length > contactformModel.MaxDescriptionLength) return BadRequest("Description is too long!");
            if (contactformModel.Subject.Length > contactformModel.MaxSubjectLength) return BadRequest("Subject is too long!");
            if (!contactformModel.IsValidEmail(contactformModel.Email)) return BadRequest("Not a valid email!");
            
            _context.Contactforms.Add(contactformModel);
            await _context.SaveChangesAsync();

            contactformModel.SendMailAsync(contactformModel.Email).Wait();

            return Ok(contactformModel);
        }

        // DELETE: api/ContactformModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactformModel(int id)
        {
            var contactformModel = await _context.Contactforms.FindAsync(id);
            if (contactformModel == null)
            {
                return NotFound();
            }

            _context.Contactforms.Remove(contactformModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactformModelExists(int id)
        {
            return _context.Contactforms.Any(e => e.Id == id);
        }
    }
}
