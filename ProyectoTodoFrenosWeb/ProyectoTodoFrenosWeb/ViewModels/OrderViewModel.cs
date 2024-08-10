namespace ProyectoTodoFrenosWeb.ViewModels
{
    public class OrderViewModel
    {
        public long OrderId { get; set; }
        public string UserId { get; set; }
        public long CodigoOrder { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RetirementDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<OrderDetailViewModel> OrderDetails { get; set; }
    }
}
