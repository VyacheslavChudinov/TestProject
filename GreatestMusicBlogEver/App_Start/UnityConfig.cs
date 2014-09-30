using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using EFManager;
using Interfaces;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Service.Authentication;
using Unity.WebApi;

namespace Service
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
			var container = new UnityContainer();

            container.RegisterType<IAuthenticationService, AuthenticationService>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserRepository, UserRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ITokenRepository, TokenRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<DbContext, MusicBlogDbContext>(new HierarchicalLifetimeManager());

            config.DependencyResolver = new UnityDependencyResolver(container);            

            
            //var oldProvider = FilterProviders.Providers.Single(f => f is FilterAttributeFilterProvider);
            //FilterProviders.Providers.Remove(oldProvider);
            //var newProvider = new UnityFilterAttributeFilterProvider(container);
            //FilterProviders.Providers.Add(newProvider);


        }
    }
}