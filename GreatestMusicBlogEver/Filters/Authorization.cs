using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Interfaces;

namespace Service.Filters
{
    public class Authorization : ActionFilterAttribute
    {
        private const string AuthCookieName = "AuthorizationCookie";
        private readonly IUserRepository userRepository;
        private readonly ITokenRepository tokenRepository;
        
        public Authorization()
        {
            var dependencyResolver = GlobalConfiguration.Configuration.DependencyResolver;

            userRepository = dependencyResolver.GetService(typeof(IUserRepository)) as IUserRepository;
            tokenRepository = dependencyResolver.GetService(typeof(ITokenRepository)) as ITokenRepository;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var header = HttpContext.Current.Request.Headers[AuthCookieName];
            var cookie = HttpContext.Current.Request.Cookies[AuthCookieName];            
            var cookieToken = cookie != null ? cookie.Value : null;            


            if (string.IsNullOrEmpty(cookieToken) && string.IsNullOrEmpty(header))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return;
            }

            var allTokens = tokenRepository.GetAll().ToList();
            var dbToken = allTokens.SingleOrDefault(t => t.Value == header) 
                ?? allTokens.SingleOrDefault(t => t.Value == cookieToken);

            if (dbToken == null || dbToken.ExpireDate < DateTime.Now || userRepository.Get(dbToken.UserId) == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return;
            }

            dbToken.Value = tokenRepository.GenerateToken();
            tokenRepository.Update(dbToken);

            HttpContext.Current.Response.Headers[AuthCookieName] = dbToken.Value;
            HttpContext.Current.Response.SetCookie(new HttpCookie(AuthCookieName, dbToken.Value));

            base.OnActionExecuting(actionContext);
        }
    }
}