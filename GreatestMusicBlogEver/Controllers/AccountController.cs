using System.Web.Http;
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

        [HttpGet]
        public IHttpActionResult Login(int id)
        {
            var user = authenticationService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            authenticationService.Login(user, false);
            return Ok(user);
        }

        public IHttpActionResult Logout()
        {
            authenticationService.Logoff();
            return Ok();
        }

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
            var registeredUser = authenticationService.Register(user);
            authenticationService.Login(registeredUser, false);
            return Ok("Success");
        }

    }
}
