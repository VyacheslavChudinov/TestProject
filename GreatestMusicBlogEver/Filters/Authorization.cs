using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Interfaces;
using Service.Exceptions;

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
            var x = actionContext.Request.Headers;
            var header = HttpContext.Current.Request.Headers[AuthCookieName];
            var cookie = HttpContext.Current.Request.Cookies[AuthCookieName];
            var cookieToken = cookie != null ? cookie.Value : null;

            try
            {
                if (string.IsNullOrEmpty(cookieToken) && string.IsNullOrEmpty(header))
                {
                    throw new MusicBlogException("User is not authorized.");
                }

                var allTokens = tokenRepository.GetAll().ToList();
                var dbToken = allTokens.SingleOrDefault(t => t.Value == header)
                              ?? allTokens.SingleOrDefault(t => t.Value == cookieToken);

                if (dbToken == null || dbToken.ExpireDate < DateTime.Now || userRepository.Get(dbToken.UserId) == null)
                {
                    throw new MusicBlogException("User is not authorized.");
                }

                dbToken.Value = tokenRepository.GenerateToken();
                tokenRepository.Update(dbToken);

                HttpContext.Current.Response.Headers[AuthCookieName] = dbToken.Value;
                HttpContext.Current.Response.SetCookie(new HttpCookie(AuthCookieName, dbToken.Value));

                base.OnActionExecuting(actionContext);
            }

            catch (Exception ex)
            {                
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);        
            }
        }
    }
}