namespace WebApi.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string ShippingAddress { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); 
    }
}
