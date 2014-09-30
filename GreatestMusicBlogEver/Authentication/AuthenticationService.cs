using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities.Concrete;
using Interfaces;
using Service.Exceptions;

namespace Service.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private const string AuthCookieName = "AuthorizationCookie";

        private readonly IUserRepository userRepository;
        private readonly ITokenRepository tokenRepository;

        public AuthenticationService(IUserRepository userRepository, ITokenRepository tokenRepository)
        {
            this.userRepository = userRepository;
            this.tokenRepository = tokenRepository;
        }

        public User Register(User user)
        {
            var userExist = userRepository.FindAll(u => u.Login == user.Login || u.Password == user.Password) != null;
            if (userExist)
            {
                throw new UserAlreadyExistException("Can't register user with login or password that already used.");
            }
            var newUser = new User {Login = user.Login, Password = user.Password};
            return userRepository.Add(newUser);
        }

        public void Login(User user, bool rememberMe)
        {
            var expiresDate = DateTime.Now.AddMinutes(30);
            if (rememberMe)
            {
                expiresDate = expiresDate.AddDays(1);
            }            

            AddTokenForUser(user, expiresDate);            
        }

        public void Logoff()
        {
            HttpContext.Current.Response.Cookies.Remove(AuthCookieName);
            HttpContext.Current.Response.Headers.Remove(AuthCookieName);
        }

        public void AddTokenForUser(User user, DateTime dateStoreTo)
        {
            var userToken = tokenRepository.FindAll(t => t.UserId == user.Id).SingleOrDefault();
            if (userToken == null)
            {
                userToken = new Token
                {
                    User = user,
                    Value = tokenRepository.GenerateToken(),
                    ExpireDate = dateStoreTo
                };

                tokenRepository.Add(userToken);
            }
            else
            {
                userToken.Value = tokenRepository.GenerateToken();
                userToken.ExpireDate = dateStoreTo;
                tokenRepository.Update(userToken);
            }

            HttpContext.Current.Response.SetCookie(new HttpCookie(AuthCookieName, userToken.Value));
            HttpContext.Current.Response.Headers.Add(AuthCookieName, userToken.Value);
        }

        public IEnumerable<User> GetAllUsers()
        {

            return userRepository.GetAll();
        }

        public User GetUser(int id)
        {
            return userRepository.Get(id);
        }


    }
}