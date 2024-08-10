using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using Azure.Core;
using API.DTO;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartsController : ControllerBase
    {
        private readonly TodoFrenosDbContext _context;

        public ShoppingCartsController(TodoFrenosDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetCartItems/{userId}")]
        public async Task<IActionResult> GetCartItems(string userId)
        {
            var cart = await _context.ShoppingCarts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.CartItems.Any())
            {
                return Ok(new List<object>());
            }

            var cartItems = cart.CartItems.Select(ci => new
            {
                ci.CartItemId,
                ci.CartId,
                ci.ProductId,
                ci.Product.ProductName,
                ci.Product.Price, 
                ci.Quantity,
                ci.Product.Stock,
                ci.Product.ImageProduct                
            }).ToList();

            return Ok(cartItems);
        }

        // PUT: api/ShoppingCarts/5
        [HttpPut("UpdateQuantity")]
        public async Task<IActionResult> PutShoppingCart([FromBody] UpdateQuantityDTO updateQuantityDto)
        {
            var cartItem = await _context.CartItems
                .FindAsync(updateQuantityDto.CartItemId);

            if (cartItem == null)
            {
                return NotFound();
            }

            cartItem.Quantity = updateQuantityDto.Quantity;
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ShoppingCarts
        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> PostShoppingCart([FromBody] CartItemDTO cartItemDto)
        {
            var cart = await _context.ShoppingCarts
                .FirstOrDefaultAsync(c => c.UserId == cartItemDto.UserId);

            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = cartItemDto.UserId,
                    DateAdd = DateTime.Now
                };
                _context.ShoppingCarts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var existingCartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cart.CartId && ci.ProductId == cartItemDto.ProductId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += cartItemDto.Quantity;
                _context.CartItems.Update(existingCartItem);
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.CartId,
                    ProductId = cartItemDto.ProductId,
                    Quantity = cartItemDto.Quantity
                };
                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Producto añadido al carrito" });
        }


        [HttpDelete("DeleteProductCart/{cartItemId}/{productId}")]
        public async Task<IActionResult> DeleteProductCart(long cartItemId, long productId)
        {
            var cartItem = await _context.CartItems
                .Include(ci => ci.Product)
                .Include(ci => ci.ShoppingCart)
                .SingleOrDefaultAsync(ci => ci.CartItemId == cartItemId);

            if (cartItem == null)
            {
                return NotFound();
            }

            if (cartItem.ProductId != productId)
            {
                return BadRequest("El ID del producto no coincide con el CartItem proporcionado."); // ID del producto no coincide
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("ClearCart/{cartId}")]
        public async Task<IActionResult> ClearCart(long cartId)
        {
            var shoppingCart = await _context.ShoppingCarts
                .Include(sc => sc.CartItems)
                .SingleOrDefaultAsync(sc => sc.CartId == cartId);

            if (shoppingCart == null)
            {
                return NotFound();
            }

            _context.CartItems.RemoveRange(shoppingCart.CartItems);
            _context.ShoppingCarts.Remove(shoppingCart);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
