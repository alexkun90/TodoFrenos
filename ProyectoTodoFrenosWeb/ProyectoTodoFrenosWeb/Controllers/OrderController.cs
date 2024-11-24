using DAL.Models;
using DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoTodoFrenosWeb.ConsumoServices;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ProyectoTodoFrenosWeb.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoTodoFrenosWeb.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class OrdersController : Controller
    {
        private readonly OrderService _orderService;
        private readonly ShoppingCartService _shoppingCartService;
        private readonly ProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HttpClientService clientService;

        public OrdersController(IConfiguration config, UserManager<ApplicationUser> userManager, HttpClientService clientService)
        {
            _orderService = new OrderService(config, clientService);
            _shoppingCartService = new ShoppingCartService(config, clientService);
            _productService = new ProductService(config, clientService);
            _userManager = userManager;
        }

        // Confirmar compra y crear la orden
        public async Task<IActionResult> ConfirmOrder(List<CartItemDTO> cartItems, string userId, long cardId)
        {
            var cartItem = await _shoppingCartService.GetCartItems(userId);
            
            
            if (cartItems == null || !cartItems.Any())
            {
                TempData["Message"] = "Tu carrito está vacío.";
                return RedirectToAction("Index", "ShoppingCarts", new { id = userId });
            }

            var order = new Order
            {
                UserId = userId,
                OrderState = 1,
                OrderDate = DateTime.Now,
                RetirementDate = DateTime.Now.AddDays(3),
                SubTotal = cartItem.Sum(ci => ci.Price * ci.Quantity),
                Tax = 0.13m * cartItem.Sum(ci => ci.Price * ci.Quantity),
                Total = cartItem.Sum(ci => ci.Price * ci.Quantity) * 1.13m,

                OrderDetails = cartItem.Select(ci => new OrderDetail
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    PriceWithTax = ci.Price * 1.13m,
                    Subtotal = ci.Price * ci.Quantity,
                    Total = (ci.Price * ci.Quantity) * 1.13m,
                }).ToList()
            };

            var result = await _orderService.CreateOrder(order);

            foreach (var item in cartItem)
            {
                _ = _productService.UpdateStock(item.ProductId, item.Quantity);
               
            }
            _ = _shoppingCartService.ClearCart(cardId);

            TempData["Message"] = result;
            return RedirectToAction("OrderList");
        }

        public ActionResult Index()
        {
            return View();
        }

        //public async Task<IActionResult> AllOrderList()
        //{
        //    var listResult = await _orderService.GetOrderList();
        //    return View(listResult);
        //}
        
        public async Task<IActionResult> GetDelayedOrders()
        {
            var listResult = await _orderService.GetDelayedOrders();
            return View(listResult);
        }
        public async Task<IActionResult> GetPendingOrders()
        {
            var listResult = await _orderService.GetPendingOrders();
            return View(listResult);
        }
        public async Task<IActionResult> GetDeliveredOrders()
        {
            var listResult = await _orderService.GetDeliveredOrders();
            return View(listResult);
        }

        public async Task<IActionResult> OrderList(string userId)
        {
            userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var listResult = await _orderService.GetMyOrderList(userId);

            return View(listResult);
        }

        public async Task<IActionResult> OrderDetailList(long? orderId)
        {
            var listResult = await _orderService.GetMyOrderDetailList(orderId);

            return View(listResult);
        }

        public async Task<IActionResult> OrderDelivered(long orderId)
        {
            try
            {
                var result = await _orderService.OrderDelivered(orderId);
                if (result)
                {
                    TempData["SuccessMessage"] = "Orden entregada correctamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Hubo un problema al entregar la orden.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hubo un error al procesar la solicitud: {ex.Message}";
            }

            return RedirectToAction(nameof(GetDeliveredOrders));
        }
    }
}
