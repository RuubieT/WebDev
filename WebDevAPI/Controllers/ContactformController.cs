using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Audit.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDevAPI.Db;
using WebDevAPI.Db.Dto_s.Contactform;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories;
using WebDevAPI.Db.Repositories.Contract;

namespace WebDevAPI.Controllers
{
    [Route("api/Contactform")]
    [ApiController]
    public class ContactformController : BaseController
    {
        public ContactformController(IContactFormRepository contactFormRepository, IPlayerRepository playerRepository, ICardRepository cardRepository,
            IPlayerHandRepository playerHandRepository, IPokerTableRepository pokerTableRepository, ILogger<BaseController> logger, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, AuditScopeFactory auditScopeFactory) : base(contactFormRepository, playerRepository, cardRepository,
            playerHandRepository, pokerTableRepository, userManager, logger, auditScopeFactory)
        {

        }

        // GET: api/ContactformModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetContactformDto>>> GetContactforms()
        {
            var contactforms = await ContactFormRepository.GetAll();
            var getContactforms = new List<GetContactformDto>();
            if (contactforms == null)
            {
                return NotFound();
            } else
            {
                foreach (var contactform in contactforms)
                {
                    getContactforms.Add(contactform.GetContactformDto());
                }
            }
           
            return Ok(getContactforms);
        }

        // GET: api/ContactformModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetContactformDto>> GetContactformModel(Guid id)
        {
            var contactformModel = await ContactFormRepository.Get(id);

            if (contactformModel == null)
            {
                return NotFound();
            }

            return Ok(contactformModel.GetContactformDto());
        }

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

            await ContactFormRepository.Create(contactform);

            //contactform.SendMailAsync(contactformModel.Email).Wait();

            return Ok(contactform.GetContactformDto());
        }

        private bool ContactformModelExists(Guid id)
        {
            return ContactFormRepository.TryFind(e => e.Id == id).Result.succes;   
        }
    }
}
