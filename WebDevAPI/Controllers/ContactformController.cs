using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDevAPI.Db;
using WebDevAPI.Db.Dto_s.Contactform;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories;

namespace WebDevAPI.Controllers
{
    [Route("api/Contactform")]
    [ApiController]
    public class ContactformController : ControllerBase
    {
        private readonly WebDevDbContext _context;

        public ContactformController(WebDevDbContext context)
        {
            _context = context;
        }

        // GET: api/ContactformModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetContactformDto>>> GetContactforms()
        {
            var contactforms = await _context.Contactforms.ToListAsync();
            var getContactforms = new List<GetContactformDto>();
            foreach (var contactform in contactforms) {
                getContactforms.Add(contactform.GetContactformDto());
            }
            return Ok(getContactforms);
        }

        // GET: api/ContactformModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetContactformDto>> GetContactformModel(Guid id)
        {
            var contactformModel = await _context.Contactforms.FindAsync(id);

            if (contactformModel == null)
            {
                return NotFound();
            }

            return Ok(contactformModel.GetContactformDto());
        }

        #region put endpoint
        // PUT: api/ContactformModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactformModel(Guid id, Contactform contactformModel)
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
        #endregion 


        // POST: api/ContactformModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GetContactformDto>> PostContactformModel(PostContactformDto contactformModel)
        {
            if (contactformModel == null) return NotFound();
            var contactform = new Contactform(new Guid(), contactformModel.Name, contactformModel.Email, contactformModel.Subject, contactformModel.Description);

            if (contactformModel.Description.Length > contactform.MaxDescriptionLength) return BadRequest("Description is too long!");
            if (contactformModel.Subject.Length > contactform.MaxSubjectLength) return BadRequest("Subject is too long!");
            if (!contactform.IsValidEmail(contactformModel.Email)) return BadRequest("Not a valid email!");

            _context.Contactforms.Add(contactform);
            await _context.SaveChangesAsync();

            contactform.SendMailAsync(contactformModel.Email).Wait();

            return Ok(contactform.GetContactformDto());
        }

        #region delete endpoint
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
        #endregion

        private bool ContactformModelExists(Guid id)
        {
            return _context.Contactforms.Any(e => e.Id == id);
        }
    }
}
