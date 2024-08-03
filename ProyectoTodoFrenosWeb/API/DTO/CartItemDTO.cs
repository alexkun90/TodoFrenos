namespace API.DTO
{
    public class CartItemDTO
    {
        public string UserId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
