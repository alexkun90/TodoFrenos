using Microsoft.AspNetCore.Mvc;

namespace ProyectoTodoFrenosWeb.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public IActionResult InicioSesion()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegistroUsuario()
        {
            return View();
        }
    }
}
