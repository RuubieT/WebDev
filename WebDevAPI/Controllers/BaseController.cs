﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebDevAPI.Db.Repositories.Contract;

namespace WebDevAPI.Controllers
{ 
    public class BaseController : Controller
    {
        protected readonly IContactFormRepository ContactFormRepository;
        protected readonly IUserRepository UserRepository;
        protected readonly IPlayerRepository PlayerRepository;
        protected readonly IPokerTableRepository PokerTableRepository;

        public BaseController(IContactFormRepository contactFormRepository, IUserRepository userRepository, IPlayerRepository playerRepository, IPokerTableRepository pokerTableRepository) 
        {
            ContactFormRepository = contactFormRepository;
            UserRepository = userRepository;
            PlayerRepository = playerRepository;
            PokerTableRepository = pokerTableRepository; 
        }

    }
}
