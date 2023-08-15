namespace WebApi.Models
{
    public class OrderRequest
    {
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string ShippingAddress { get; set; }
    }
}
