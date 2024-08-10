namespace ProyectoTodoFrenosWeb.ViewModels
{
    public class CartItemViewModel
    {
        public long CartItemId { get; set; }
        public long CartId { get; set; }    
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Stock { get; set; }
        public byte[]? ImageProduct { get; set; }
    }
}
