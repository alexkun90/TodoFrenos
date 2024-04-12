using Microsoft.AspNetCore.Mvc;

namespace ProyectoTodoFrenosWeb.Controllers
{
    public class CarritoController : Controller
    {
        public IActionResult ListaCarrito()
        {
            return View();
        }
    }
}
