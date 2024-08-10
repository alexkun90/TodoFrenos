namespace ProyectoTodoFrenosWeb.ViewModels
{
    public class CartItemDTO
    {
        public long CartItemId { get; set; }
        public long CartId { get; set; }
        public string UserId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal PriceWithTax { get; set; }

    }
}
