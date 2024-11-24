using API.DTO;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly TodoFrenosDbContext _context;

        public OrdersController(TodoFrenosDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (order == null || !order.OrderDetails.Any())
            {
                return BadRequest("Datos inválidos para la orden.");
            }

            order.OrderDate = DateTime.Now;
            order.CodigoOrder = GenerateOrderCode();

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Orden creada con éxito", order.OrderId });
        }

        [HttpGet("GetDelayedOrders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetDelayedOrders()
        {
            return await _context.Orders
                .Where(o => o.OrderState == 0)
                .Include(o => o.User)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        [HttpGet("GetPendingOrders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetPendingOrders()
        {
            return await _context.Orders
                .Where(o => o.OrderState == 1)
                .Include(o => o.User)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        [HttpGet("GetDeliveredOrders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetDeliveredOrders()
        {
            return await _context.Orders
                .Where(o => o.OrderState == 2)
                .Include(o => o.User)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        [HttpGet("MyOrders/{userId}")]
        public async Task<ActionResult<IEnumerable<Order>>>GetMyOrders(string userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }


        [HttpGet("GetMyDetailsOrders/{orderId}")]
        public async Task<IActionResult> GetMyDetailsOrder(long orderId)
        {
            var orderDetails = await _context.OrderDetails
                .Where(o => o.OrderId == orderId)
                .Include(o => o.Product)
                .ToListAsync();

            if (orderDetails == null || !orderDetails.Any())
            {
                return Ok(new List<object>());
            }

            var result = orderDetails.Select(o => new OrderDetailDTO
            {
                OrderDetailId = o.OrderDetailId, 
                OrderId = o.OrderId,
                ProductId = o.ProductId,
                ProductName = o.Product?.ProductName,
                Price = o.Product?.Price,
                PriceWithTax = o.PriceWithTax,
                Quantity = o.Quantity,
                Subtotal = o.Subtotal,
                Total = o.Total
            }).ToList();

            return Ok(result);
        }

        [HttpPut("Delivered/{orderId}")]
        public async Task<IActionResult> OrderDelivered(long orderId)
        {
            try
            {
                var order = await _context.Orders.FindAsync(orderId);
                if (order == null)
                {
                    return NotFound(new { message = "Orden no encontrada." });
                }

                order.OrderState = 2;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Orden entregada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al cambiar el estado de la orden: {ex.Message}" });
            }
        }

        private long GenerateOrderCode()
        {
            int year = DateTime.Now.Year % 100;
            int dayOfYear = DateTime.Now.DayOfYear; 
            int randomPart = new Random().Next(100, 999);
            long orderCode = long.Parse($"{year}{dayOfYear:D3}{randomPart}");
            return orderCode;
        }
    }
}
