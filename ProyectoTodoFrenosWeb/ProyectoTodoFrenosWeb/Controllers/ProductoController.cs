using Microsoft.AspNetCore.Mvc;

namespace ProyectoTodoFrenosWeb.Controllers
{
    public class ProductoController : Controller
    {
        [HttpGet]
        public IActionResult ListaProductos()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminProductos()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AgregarProductos()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ModificarProductos()
        {
            return View();
        }
    }
}
