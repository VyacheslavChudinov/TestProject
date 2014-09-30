using System;
using System.Web.Http;
using System.Web.Http.Results;
using Entities.Concrete;
using Interfaces;
using Service.Filters;

namespace Service.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IAuthenticationService authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost]
        public IHttpActionResult Login(string login, string password)
        {
            var user = authenticationService.GetUser(login, password);
            if (user == null)
            {
                return NotFound();
            }
            authenticationService.Login(user, false);

            return Ok(user);
        }

        //public IHttpActionResult Logout()
        //{
        //    authenticationService.Logoff();
        //    return Ok();
        //}

        [HttpGet]
        [Authorization]
        public IHttpActionResult GetAll()
        {

            var result = authenticationService.GetAllUsers();
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult Register([FromBody] User user)
        {
            User registeredUser;
            try
            {
                 registeredUser = authenticationService.Register(user);                    
            }
            catch (Exception ex)
            {                
                return new ExceptionResult(ex, this);
            }
            
            authenticationService.Login(registeredUser, false);
            return Ok("Success");
        }

    }
}
