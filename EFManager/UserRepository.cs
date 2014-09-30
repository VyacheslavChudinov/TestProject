using System.Data.Entity;
using Entities.Concrete;
using Interfaces;

namespace EFManager
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext):base(dbContext)
        {
            
        }
    }
}
