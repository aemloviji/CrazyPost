using CrazyPost.Models;
using Microsoft.EntityFrameworkCore;

namespace CrazyPost.Contexts
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options) { }
        public ApiDbContext() { }

        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }
     
    }
}
