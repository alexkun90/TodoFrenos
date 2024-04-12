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

        [HttpGet]
        public IActionResult CodigoAcceso()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NuevaContrasenna()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PerfilUsuario()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ActualizarUsuario()
        {
            return View();
        }

        /*CRUD*/
        [HttpGet]
        public IActionResult AdminUsuarios()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AgregarUsuarios()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ModificarUsuarios()
        {
            return View();
        }
    }
}
