using CrazyPost.Models;
using Microsoft.EntityFrameworkCore;

namespace CrazyPost.Contexts
{
    public class CrazyPostContext : DbContext
    {
        public CrazyPostContext(DbContextOptions<CrazyPostContext> options)
            : base(options) { }
        public CrazyPostContext() { }

        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }
     
    }
}
