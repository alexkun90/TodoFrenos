using DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProyectoTodoFrenosWeb.Controllers
{
    public class UsuarioController : Controller
    {
        
        private readonly UserManager<ApplicationUser> gestionUsuarios;
        private readonly RoleManager<IdentityRole> gestionRoles;

        public UsuarioController(UserManager<ApplicationUser> gestionUsuarios, RoleManager<IdentityRole> gestionRoles)
        {
            this.gestionRoles = gestionRoles;
            this.gestionUsuarios = gestionUsuarios;
        }
        [HttpGet]
        public IActionResult ListaUsuarios()
        {
            var usuarios = gestionUsuarios.Users;

            return View(usuarios);
        }
       

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
