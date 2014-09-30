using Entities.Concrete;

namespace Interfaces
{
    public interface ITokenRepository : IBaseRepository<Token>
    {
        string GenerateToken();
    }
}
