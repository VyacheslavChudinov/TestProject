using System;
using System.Data.Entity;
using Entities.Concrete;
using Interfaces;

namespace EFManager
{
    public class TokenRepository : BaseRepository<Token>, ITokenRepository
    {
        private readonly Random random = new Random();

        public TokenRepository(DbContext dbContext):base(dbContext)
        {
            
        }

        public string GenerateToken()
        {
            return random.Next(Int32.MaxValue).ToString();
        }        
    }
}
