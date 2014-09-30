using System.Data.Entity;
using Entities.Concrete;

namespace EFManager
{
    public class MusicBlogDbContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }

        public MusicBlogDbContext()
            : base("MusicBlogDBContext")
        {

        }
    }
}