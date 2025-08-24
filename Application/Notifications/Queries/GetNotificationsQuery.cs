using hahn.Application.DTOs;
using hahn.Application.Validators;
using MediatR;

namespace hahn.notifications.Queries {
    public class GetNotificationsQuery : IRequest<CustomResult<NotificationDTO>>
    {
        public int notifiedId { get; set; }
    }
}