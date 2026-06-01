using Microsoft.EntityFrameworkCore;
using BlogCMS.Models;

namespace BlogCMS.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }

        // tabela z postami w bazie
        public DbSet<Post> Posts { get; set; }
    }
}
