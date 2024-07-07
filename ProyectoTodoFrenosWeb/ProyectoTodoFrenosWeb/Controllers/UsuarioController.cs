using DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProyectoTodoFrenosWeb.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly AuthDbContext _authDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsuarioController(AuthDbContext authDbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _authDbContext = authDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> ListaUsuarios()
        {
            
            return View(await _authDbContext.Users.ToListAsync());
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
