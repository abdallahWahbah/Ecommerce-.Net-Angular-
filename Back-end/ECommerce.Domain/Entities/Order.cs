namespace ECommerce.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal Discount { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
