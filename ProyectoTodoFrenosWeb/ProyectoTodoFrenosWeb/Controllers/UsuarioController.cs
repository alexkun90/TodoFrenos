using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoTodoFrenosWeb.ConsumoServices;
using ProyectoTodoFrenosWeb.ViewModels;

namespace ProyectoTodoFrenosWeb.Controllers
{
    [Authorize(Roles = "Admin,Mecanico")]
    public class UsuarioController : Controller
    {
        
        private readonly UserManager<ApplicationUser> gestionUsuarios;
        private readonly RoleManager<IdentityRole> gestionRoles;

       

        public UsuarioController(UserManager<ApplicationUser> gestionUsuarios, RoleManager<IdentityRole> gestionRoles)
        {
            this.gestionRoles = gestionRoles;
            this.gestionUsuarios = gestionUsuarios;
            
        }
       
        public async Task<IActionResult> ListaUsuarios()
        {
            var usuarios = gestionUsuarios.Users.ToList();
            var usuariosConRoles = new List<EditarUsuarioViewModel>();

            foreach (var usuario in usuarios)
            {
                var roles = await gestionUsuarios.GetRolesAsync(usuario);
                usuariosConRoles.Add(new EditarUsuarioViewModel
                {
                    Id = usuario.Id,
                    NombreUsuario = usuario.Nombre,
                    PrimApellido = usuario.PrimApellido,
                    SegunApellido = usuario.SegunApellido,
                    Email = usuario.Email,
                    Activo = usuario.Activo,
                    Roles = roles
                });
            }
            var user = await gestionUsuarios.GetUserAsync(User);
            ViewBag.CurrentUser = user;

            return View(usuariosConRoles);
        }

        [HttpGet]
        public async Task<IActionResult> EditarUsuario(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await gestionUsuarios.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            var roles = await gestionUsuarios.GetRolesAsync(usuario);

            var todosLosRoles = gestionRoles.Roles.Select(r => r.Name).ToList();

            ViewBag.Roles = todosLosRoles;

            var modelo = new EditarUsuarioViewModel
            {
                Id = usuario.Id,
                NombreUsuario = usuario.Nombre,
                PrimApellido = usuario.PrimApellido,
                SegunApellido = usuario.SegunApellido,
                Email = usuario.Email,
                Activo = usuario.Activo,
                Roles = roles
            };

            return View(modelo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarUsuario(EditarUsuarioViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var usuario = await gestionUsuarios.FindByIdAsync(modelo.Id);
            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Nombre = modelo.NombreUsuario;
            usuario.PrimApellido = modelo.PrimApellido;
            usuario.SegunApellido = modelo.SegunApellido;
            usuario.Email = modelo.Email;
            usuario.Activo = modelo.Activo;

            var resultado = await gestionUsuarios.UpdateAsync(usuario);

            if (resultado.Succeeded)
            {
                // Actualizar los roles del usuario
                var rolesActuales = await gestionUsuarios.GetRolesAsync(usuario);
                var resultadoRoles = await gestionUsuarios.RemoveFromRolesAsync(usuario, rolesActuales);
                if (!resultadoRoles.Succeeded)
                {
                    ModelState.AddModelError("", "Error al actualizar roles");
                    return View(modelo);
                }

                resultadoRoles = await gestionUsuarios.AddToRolesAsync(usuario, modelo.Roles);
                if (!resultadoRoles.Succeeded)
                {
                    ModelState.AddModelError("", "Error al actualizar roles");
                    return View(modelo);
                }

                return RedirectToAction(nameof(ListaUsuarios));
            }

            foreach (var error in resultado.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(modelo);
        }


        [HttpGet]
        public IActionResult CrearUsuario()
        {
            var roles = gestionRoles.Roles.Select(r => r.Name).ToList();
            var modelo = new RegistroModelo
            {
                Roles = new List<string>()
            };
            ViewBag.Roles = roles;
            return View(modelo);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearUsuario(RegistroModelo modelo)
        {
            if (!ModelState.IsValid)
            {
                var roles = gestionRoles.Roles.Select(r => r.Name).ToList();
                ViewBag.Roles = roles;
                return View(modelo);
            }

            var usuario = new ApplicationUser
            {
                Nombre = modelo.NombreUsuario,
                UserName = modelo.Email,
                Email = modelo.Email,
                PrimApellido = modelo.PrimApellido,
                SegunApellido = modelo.SegunApellido,
                Activo = modelo.Activo
            };

            var resultado = await gestionUsuarios.CreateAsync(usuario, modelo.Password); // Asegúrate de pedir una contraseña segura en el formulario real

            if (resultado.Succeeded)
            {
                if (modelo.Roles != null && modelo.Roles.Count > 0)
                {
                    var resultadoRoles = await gestionUsuarios.AddToRolesAsync(usuario, modelo.Roles);
                    if (!resultadoRoles.Succeeded)
                    {
                        ModelState.AddModelError("", "Error al asignar roles");
                        var roles = gestionRoles.Roles.Select(r => r.Name).ToList();
                        ViewBag.Roles = roles;
                        return View(modelo);
                    }
                }

                return RedirectToAction(nameof(ListaUsuarios));
            }

            foreach (var error in resultado.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            var todosLosRoles = gestionRoles.Roles.Select(r => r.Name).ToList();
            ViewBag.Roles = todosLosRoles;
            return View(modelo);
        }




        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            var usuario = await gestionUsuarios.FindByIdAsync(id);
            
            if (usuario == null)
            {
                // Si el usuario no existe, lanzamos una excepción
                throw new Exception("La usuario no se encontró en la base de datos.");
            }

            usuario.Activo = false;
            
            await gestionUsuarios.UpdateAsync(usuario);

            return RedirectToAction(nameof(ListaUsuarios));
        }

        // GET: Products/Activate/5
        public async Task<IActionResult> Activate(string? id)
        {
            var usuario = await gestionUsuarios.FindByIdAsync(id);

            if (usuario == null)
            {
                // Si el usuario no existe, lanzamos una excepción
                throw new Exception("La usuario no se encontró en la base de datos.");
            }

            usuario.Activo = true;

            await gestionUsuarios.UpdateAsync(usuario);

            return RedirectToAction(nameof(ListaUsuarios));
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
