using Microsoft.AspNetCore.Mvc;

namespace ProyectoTodoFrenosWeb.Controllers
{
    public class PedidosController : Controller
    {
        [HttpGet]
        public IActionResult HistorialPedidos()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminPedidos()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DetallePedidos()
        {
            return View();
        }
    }
}
