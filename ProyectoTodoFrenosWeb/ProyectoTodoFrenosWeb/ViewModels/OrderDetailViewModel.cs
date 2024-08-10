namespace ProyectoTodoFrenosWeb.ViewModels
{
    public class OrderDetailViewModel
    {
        public long OrderDetailId { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceWithTax { get; set; }
        public int? Quantity { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Total { get; set; }
    }
}
