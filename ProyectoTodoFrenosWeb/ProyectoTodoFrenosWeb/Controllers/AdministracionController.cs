using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoTodoFrenosWeb.ViewModels;

namespace ProyectoTodoFrenosWeb.Controllers
{
    public class AdministracionController : Controller
    {
        private readonly RoleManager<IdentityRole> gestionRoles;
        private readonly UserManager<ApplicationUser> gestionUsuarios;

        public AdministracionController(RoleManager<IdentityRole> gestionRoles, UserManager<ApplicationUser> gestionUsuarios)
        {
            this.gestionRoles = gestionRoles;
            this.gestionUsuarios = gestionUsuarios;
        }

        [HttpGet]
        public IActionResult CrearRol()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearRol(CrearRolViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.NameRol
                };
                IdentityResult result = await gestionRoles.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Administracion");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return View(model);
        }
        // GET: AdministracionController
        public IActionResult Index()
        {
            var roles = gestionRoles.Roles;
            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> EditarRol(string id)
        {
            //Buscamos rol por Id
            var rol = await gestionRoles.FindByIdAsync(id);

            if (rol == null)
            {
                ViewBag.ErrorMessage = $"Rol con el Id = {id} no fue encontrado";
                return View("Error");
            }
            var model = new EditarRolViewModel
            {
                Id = rol.Id,
                RolName = rol.Name
            };

            //Obtenemos todos los Usuarios

            foreach (var usuario in gestionUsuarios.Users)
            {
                if (await gestionUsuarios.IsInRoleAsync(usuario, rol.Name))
                {
                    model.Usuarios.Add(usuario.UserName);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditarRol(EditarRolViewModel model)
        {
            var rol = await gestionRoles.FindByIdAsync (model.Id);
            if (rol == null)
            {
                ViewBag.ErrorMessage = $"Rol con el Id = {model.Id} no fue encontrado";
                return View("Error");
            }
            else
            {
                rol.Name = model.RolName;

                var resultado = await gestionRoles.UpdateAsync(rol);

                if (resultado.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }
        
        [HttpGet]    
        public async Task<IActionResult> EditarUsuarioRol(string rolId)
        {
            ViewBag.roleId = rolId;

            var role = await gestionRoles.FindByIdAsync(rolId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Rol con el Id = {rolId} no fue encontrado";
                return View("Error");
            }
            var model = new List<UsuarioRolViewModel>();
            foreach (var user in gestionUsuarios.Users)
            {
                var usuarioRolViewModel = new UsuarioRolViewModel
                {
                    UsuarioId = user.Id,
                    UsuarioName = user.UserName
                };

                if (await gestionUsuarios.IsInRoleAsync(user,role.Name))
                {
                    usuarioRolViewModel.EstaSeleccionado = true;
                }
                else
                {
                    usuarioRolViewModel.EstaSeleccionado = false;
                }
                model.Add(usuarioRolViewModel);
            }
			return View(model);
		}

        [HttpPost]
        public async Task<IActionResult> EditarUsuarioRol(List<UsuarioRolViewModel> model, string rolId)
        {
            var rol = await gestionRoles.FindByIdAsync (rolId);

            if (rol == null)
            {
                ViewBag.ErrorMessage = $"Rol con el Id = {rolId} no fue encontrado";
                return View("Error");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await gestionUsuarios.FindByIdAsync(model[i].UsuarioId);

                IdentityResult result = null;
                if (model[i].EstaSeleccionado && !(await gestionUsuarios.IsInRoleAsync(user, rol.Name)))
                {
                    result = await gestionUsuarios.AddToRoleAsync(user, rol.Name);
                }
                else if (!model[i].EstaSeleccionado && await gestionUsuarios.IsInRoleAsync(user, rol.Name))
                {
                    result = await gestionUsuarios.RemoveFromRoleAsync(user, rol.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditarRol", new { Id = rolId });
                }
            }
            return RedirectToAction("EditarRol", new {Id = rolId});
        }
    }
}
