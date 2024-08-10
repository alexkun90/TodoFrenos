using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using ProyectoTodoFrenosWeb.ConsumoServices;
using DAL;
using Microsoft.AspNetCore.Identity;
using ProyectoTodoFrenosWeb.ViewModels;
using Azure.Core;
using System.Security.Claims;

namespace ProyectoTodoFrenosWeb.Controllers
{
    public class ShoppingCartsController : Controller
    {
        ShoppingCartService _shoppingCartService;
        ProductService _productService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingCartsController(IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _shoppingCartService = new ShoppingCartService(config);
            _productService = new ProductService(config);
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = await _shoppingCartService.GetCartItems(userId);
            if (!cartItems.Any())
            {
                TempData["Message"] = "Tu carrito está vacío.";
            }
            return View(cartItems);
        }
        
        public async Task<IActionResult> UpdateQuantity([FromBody] UpdateQuantityDTO model)
        {
            var success = await _shoppingCartService.UpdateQuantity(model.CartItemId, model.Quantity);
            return Json(new { success });

            
        }

        public async Task<IActionResult> DeleteProductCart(long cartItemId, long productId)
        {
            if (cartItemId <= 0 || productId <= 0)
            {
                return NotFound();
            }

            var resultado = await _shoppingCartService.DeleteProductCart(cartItemId, productId);

            if (resultado)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Error al inactivar la cita.");
            return RedirectToAction(nameof(Index));
        }

    }
}

