using MediatR;

namespace hahn.Domain.Events
{
    public class OrderCreateEvent : INotification 
    {
        public int OrderId { get; }
        public int BuyerId { get; }
        public int SellerId { get; }

        public OrderCreateEvent(int orderId, int buyerId, int sellerId)
        {
            OrderId = orderId;
            BuyerId = buyerId;
            SellerId = sellerId;
        }
    }
}