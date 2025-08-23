using hahn.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace hahn.Infrastructure.Presistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Seller>(b =>
            {
                b.OwnsOne(s => s.adress);
                b.OwnsOne(s => s.localAdress);
                b.Property(s => s.mySource).HasDefaultValue(Sources.Other);

            });

            modelBuilder.Entity<Buyer>(b =>
            {
                b.OwnsOne(b => b.adress);
            });


        }

    }
}