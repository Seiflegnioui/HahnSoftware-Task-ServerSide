using hahn.Domain.Entities;
using hahn.Domain.Enums;
using hahn.Domain.Repositories;
using hahn.Infrastructure.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class OrderStateChangedHandler : INotificationHandler<OrderStateChangedEvent>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IOrderRepository _orderRepository;

    public OrderStateChangedHandler(INotificationRepository notificationRepository, IOrderRepository orderRepository)
    {
        _notificationRepository = notificationRepository;
        _orderRepository = orderRepository;
    }

    public async Task Handle(OrderStateChangedEvent evt, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByIdAsync(evt.OrderId, cancellationToken);

        var subject = evt.NewState == OrderState.APPROVED
            ? "Order Approved!"
            : "Order Rejected";

        var content = evt.NewState == OrderState.APPROVED
            ? $"Great news! Your order #{evt.OrderId} for '{order.Product.name}' has been approved by {order.Product.User.shopName}. " +
              $"Your {order.quantity} item(s) totaling ${order.Product.price * order.quantity} are being prepared for shipment."
            : $"We regret to inform you that your order #{evt.OrderId} for '{order.Product.name}' " +
              $"has been rejected by {order.Product.User.shopName}. " +
              $"If you have any questions, please contact the seller directly.";

        var notif = Notification.Create(
            notifiedId: evt.BuyerUserId,
            subject: subject,
            content: content,
            notifierId: evt.SellerUserId
        );

        await _notificationRepository.AddNotificationAsync(notif, cancellationToken);
    }
}
