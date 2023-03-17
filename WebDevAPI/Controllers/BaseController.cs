using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebDevAPI.Db.Repositories.Contract;

namespace WebDevAPI.Controllers
{ 
    public class BaseController : Controller
    {
        protected readonly IUserRepository UserRepository;
        protected readonly IContactFormRepository ContactFormRepository;

        public BaseController(IUserRepository userRepository, IContactFormRepository contactFormRepository) 
        {   
            UserRepository = userRepository;
            ContactFormRepository = contactFormRepository;
        }

    }
}
