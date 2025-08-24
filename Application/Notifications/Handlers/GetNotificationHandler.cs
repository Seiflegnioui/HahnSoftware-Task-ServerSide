using hahn.Application.DTOs;
using hahn.Application.Validators;
using hahn.Domain.Repositories;
using hahn.Infrastructure.Services;
using hahn.notifications.Queries;
using MediatR;

namespace hahn.notifications.Handlers {
    public class GetNotificationsHandler(INotificationRepository repository) : IRequestHandler<GetNotificationsQuery, CustomResult<NotificationDTO>>
    {
        public async Task<CustomResult<NotificationDTO>> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
        {
            var notifications = await repository.GetNotifications(request.notifiedId, cancellationToken);
            
            var dto = NotificationMapper.ToDTOList(notifications);
            return CustomResult<NotificationDTO>.Ok(dto);
        }
    }
}