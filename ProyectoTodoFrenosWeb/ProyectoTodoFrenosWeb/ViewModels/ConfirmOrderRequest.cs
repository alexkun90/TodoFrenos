namespace ProyectoTodoFrenosWeb.ViewModels
{
    public class ConfirmOrderRequest
    {
        public List<CartItemDTO> cartItems { get; set; }
        public string userId { get; set; }
        public long cardId { get; set; }
    }
}
