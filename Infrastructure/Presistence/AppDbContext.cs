using hahn.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace hahn.Infrastructure.Presistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}