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
    }
}
