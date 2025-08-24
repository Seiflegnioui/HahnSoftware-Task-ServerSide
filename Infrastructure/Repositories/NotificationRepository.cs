using hahn.Application.buyer.Commands;
using hahn.Application.DTOs;
using hahn.Application.order.Commands;
using hahn.Application.order.Queries;
using hahn.Application.Validators;
using hahn.Domain.Entities;
using hahn.Domain.Enums;
using hahn.Domain.Repositories;
using hahn.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;

namespace hahn.Infrastructure.Repositories
{

    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext context;

        public NotificationRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Notification> AddNotificationAsync(Notification notification, CancellationToken cancellationToken)
        {
            try
            {
            await context.Notifications.AddAsync(notification, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                throw;
            }
            return notification;
        }

        public Task<List<Notification>> GetNotifications(int notified, CancellationToken cancellationToken)
        {
            var notifications = context.Notifications.Where(n => n.notifiedId == notified)
            .Include(n=> n.Notified).Include(n=>n.Notifier).ToListAsync(cancellationToken);
            return notifications;
        }
    }

}