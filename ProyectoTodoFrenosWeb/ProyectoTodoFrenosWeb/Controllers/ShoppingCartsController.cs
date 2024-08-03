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
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingCartsController(IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _shoppingCartService = new ShoppingCartService(config);
            _userManager = userManager;
        }

        //Método para obtener el usuario en sesión
        // GET: ShoppingCarts
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

        // POST: ShoppingCarts/Create
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(long cartItemId, int quantity)
        {
            var success = await _shoppingCartService.UpdateQuantity(cartItemId, quantity);
            if (success)
            {
                TempData["Message"] = "Cantidad actualizada con éxito.";
            }
            else
            {
                TempData["Message"] = "Error al actualizar la cantidad.";
            }
            return RedirectToAction("Index");
        }
    }
}

