using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class OrderService
    {
        private readonly TodoFrenosDbContext _context;

        public OrderService(TodoFrenosDbContext context)
        {
            _context = context;
        }

        public async Task OrderDelayed(long orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
            {
                Console.WriteLine($"Orden {orderId} no encontrada.");
                return;
            }

            if (order.OrderState == 1)
            {
                Console.WriteLine($"Orden {orderId} ya estaba marcada como retrasada.");
                return;
            }

            order.OrderState = 1;

            foreach (var detail in order.OrderDetails)
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.ProductId == detail.ProductId);

                if (product != null)
                {
                    product.Stock += detail.Quantity;
                    _context.Products.Update(product);
                }
            }

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            Console.WriteLine($"Orden {orderId} marcada como retrasada y stock actualizado.");
        }

        public async Task ReviewAllOrders()
        {
            Console.WriteLine("Iniciando revisión de órdenes retrasadas...");

            var currentDate = DateTime.Now;

            // Obtener todas las órdenes pendientes cuya fecha de entrega ya pasó
            var delayedOrders = await _context.Orders
                .Include(o => o.OrderDetails)
                .Where(o => o.OrderState == 1 && o.RetirementDate < currentDate)
                .ToListAsync();

            if (!delayedOrders.Any())
            {
                Console.WriteLine("No se encontraron órdenes retrasadas.");
                return;
            }

            foreach (var order in delayedOrders)
            {
                order.OrderState = 0;

                foreach (var detail in order.OrderDetails)
                {
                    var product = await _context.Products
                        .FirstOrDefaultAsync(p => p.ProductId == detail.ProductId);

                    if (product != null)
                    {
                        product.Stock += detail.Quantity;
                        _context.Products.Update(product);
                    }
                }

                _context.Orders.Update(order);
            }

            await _context.SaveChangesAsync();

            Console.WriteLine($"{delayedOrders.Count} órdenes marcadas como retrasadas y stock actualizado.");
        }
    }
}

