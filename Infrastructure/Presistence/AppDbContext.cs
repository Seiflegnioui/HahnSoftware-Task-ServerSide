using hahn.Domain;
using hahn.Domain.Entities;
using hahn.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hahn.Infrastructure.Presistence
{
    public class AppDbContext : DbContext
    {
        private readonly IMediator _mediator;
        public DbSet<User> Users { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Notification> Notifications { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;

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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var events = DomainEvents.Events.ToList();
            DomainEvents.Clear();

            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var evt in events)
            {
                await _mediator.Publish(evt, cancellationToken);
            }

            return result;
        }

    }
}