using Microsoft.EntityFrameworkCore;
using TravelQuotesApi.Models;

namespace TravelQuotesApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // tabela z cytatami
        public DbSet<Quote> Quotes { get; set; }
    }
}
