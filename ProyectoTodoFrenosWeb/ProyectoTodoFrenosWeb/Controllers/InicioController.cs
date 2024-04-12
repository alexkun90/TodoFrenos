using Microsoft.AspNetCore.Mvc;

namespace ProyectoTodoFrenosWeb.Controllers
{
    public class InicioController : Controller
    {
        [HttpGet]
        public IActionResult Inicio()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contacto()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Servicios()
        {
            return View();
        }

    }
}
