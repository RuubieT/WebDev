using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebDevAPI.Db.Repositories.Contract;

namespace WebDevAPI.Controllers
{ 
    public class BaseController : Controller
    {
        protected readonly IContactFormRepository ContactFormRepository;
        protected readonly IPlayerRepository PlayerRepository;
        protected readonly ICardRepository CardRepository;
        protected readonly IPlayerHandRepository PlayerHandRepository;
        protected readonly IPokerTableRepository PokerTableRepository;
        protected readonly ILogger<BaseController> Logger;
        protected readonly UserManager<IdentityUser> UserManager;


        public BaseController(IContactFormRepository contactFormRepository, 
            IPlayerRepository playerRepository, ICardRepository cardRepository, 
            IPlayerHandRepository playerHandRepository, IPokerTableRepository pokerTableRepository, ILogger<BaseController> logger, UserManager<IdentityUser> userManager)
        {
            ContactFormRepository = contactFormRepository;
            PlayerRepository = playerRepository;
            PokerTableRepository = pokerTableRepository;
            CardRepository = cardRepository;
            PlayerHandRepository = playerHandRepository;
            Logger = logger;
            UserManager = userManager;
        }

    }
}
