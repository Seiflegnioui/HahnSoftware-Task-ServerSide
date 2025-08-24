using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using hahn.Domain.Enums;
using hahn.Domain.Events;
using hahn.Domain.ValueObject;

namespace hahn.Domain.Entities
{
    public class Order
    {
        [Key]
        public int id { get; set; }

        public int buyerId { get; set; }
        [ForeignKey(nameof(buyerId))]
        public Buyer Buyer { get; set; } = null!;


        public int productId { get; set; }

        [ForeignKey(nameof(productId))]
        public Product Product { get; set; } = null!;
        public int quantity { get; set; }
        public OrderState state { get; set; } = OrderState.PENDING;
        public DateTime addedAt { get; set; } = DateTime.Now;

        private Order() { }


        public static Order Send(int buyerId, int productId, int quantity)
        {
            if (buyerId == null) throw new ArgumentNullException(nameof(buyerId));
            if (productId == null) throw new ArgumentNullException(nameof(productId));
            if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero");

            return new Order
            {
                buyerId = buyerId,
                productId = productId,
                quantity = quantity,
                state = OrderState.PENDING,
                addedAt = DateTime.UtcNow
            };

        }

        public Order ChangeState(OrderState newState)
        {
            if (state != OrderState.PENDING)
                throw new InvalidOperationException("Only pending orders can be approved.");
            state = newState ;

            DomainEvents.Raise(new OrderStateChangedEvent(id, Buyer.userId, Product.User.userId, state));
            return this;
        }



        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero");
            quantity = newQuantity;
        }
    }




}