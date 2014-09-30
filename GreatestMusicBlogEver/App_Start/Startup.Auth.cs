using System;
using System.Data.Entity;
using EFManager;
using EFManager.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace Service
{
    public partial class Startup
    {
        static Startup()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MusicBlogDbContext, Configuration>());
        }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static Func<UserManager<IdentityUser>> UserManagerFactory { get; set; }
        
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }
    }
}
