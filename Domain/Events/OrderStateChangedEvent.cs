using MediatR;
using hahn.Domain.Enums;
using hahn.Domain;

public class OrderStateChangedEvent : INotification
{
    public int OrderId { get; }
    public int BuyerUserId { get; }
    public int SellerUserId { get; }
    public OrderState NewState { get; }

    public OrderStateChangedEvent(int orderId, int buyerUserId, int sellerUserId, OrderState newState)
    {
        OrderId = orderId;
        BuyerUserId = buyerUserId;
        SellerUserId = sellerUserId;
        NewState = newState;
    }
}
