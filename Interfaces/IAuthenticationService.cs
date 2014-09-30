using System.Collections.Generic;
using Entities.Concrete;

namespace Interfaces
{
    public interface IAuthenticationService
    {
        User Register(User user);
        void Login(User user, bool rememberMe);

        void Logoff();

        User GetUser(int id);

        IEnumerable<User> GetAllUsers();
    }
}
