using hahn.Application.DTOs;
using hahn.Domain.Entities;

namespace hahn.Infrastructure.Services
{
    public class NotificationMapper
    {
        public static NotificationDTO ToDTO(Notification n)
        {
            return new NotificationDTO
            {
                id = n.id,
                content = n.content,
                subject = n.subject,
                seen = n.seen,
                time = n.time,
                Notified = new UserDTO
                {
                    id = n.Notified.id,
                    username = n.Notified.username,
                    email = n.Notified.email
                },
                Notifier = new UserDTO
                {
                    id = n.Notifier.id ,
                    username = n.Notifier.username,
                    email = n.Notifier.email
                },

            };
        }
        

        public static List<NotificationDTO> ToDTOList(List<Notification> notifs)
        {
            return notifs.Select(n => ToDTO(n)).ToList();
        }
    }
}
