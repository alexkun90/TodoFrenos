using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ProyectoTodoFrenosWeb.ConsumoServices;
using ProyectoTodoFrenosWeb.ViewModels;
using System.Net.Mail;
using System.Text.Encodings.Web;

namespace ProyectoTodoFrenosWeb.Controllers
{
    [Authorize(Roles = "Admin,Mecanico")]
    public class UsuarioController : Controller
    {
        
        private readonly UserManager<ApplicationUser> gestionUsuarios;
        private readonly RoleManager<IdentityRole> gestionRoles;
        private readonly IEmailSender _emailSender;


        public UsuarioController(UserManager<ApplicationUser> gestionUsuarios, RoleManager<IdentityRole> gestionRoles, IEmailSender _emailSender)
        {
            this.gestionRoles = gestionRoles;
            this.gestionUsuarios = gestionUsuarios;
            this._emailSender = _emailSender;
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
            var newPassword = GenerateRandomPassword();
            var modelo = new RegistroModelo
            {
                Roles = new List<string>(),
                Password = newPassword,
                ConfirmPassword = newPassword
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
            var emailSubject = "Contraseña Temporal";
            var emailMessage = $"Gracias por confiar en nosotros, ya puedes ingresar a nuestra pagina. " + "\n" +
                               $"Tu contraseña temporal es: <strong>{modelo.Password}</strong>, al iniciar sesión podras cambiarla en la sección del perfil de usuario";
            await _emailSender.SendEmailAsync(modelo.Email, emailSubject, emailMessage);
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

        private string GenerateRandomPassword()
        {
            const int length = 12;
            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";

            // Conjunto de caracteres válidos
            var allChars = upperChars + lowerChars + numbers;

            var random = new Random();
            var newPassword = new char[length];

            // Asegúrate de que la contraseña tenga al menos un carácter de cada tipo
            newPassword[0] = upperChars[random.Next(upperChars.Length)];
            newPassword[1] = lowerChars[random.Next(lowerChars.Length)];
            newPassword[2] = numbers[random.Next(numbers.Length)];

            // Rellena el resto de la contraseña con caracteres aleatorios
            for (int i = 3; i < length; i++)
            {
                newPassword[i] = allChars[random.Next(allChars.Length)];
            }

            // Mezcla los caracteres para asegurar una distribución aleatoria
            return new string(newPassword.OrderBy(c => random.Next()).ToArray());
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
