using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using ProyectoTodoFrenosWeb.ConsumoServices;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using DAL;

namespace ProyectoTodoFrenosWeb.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly TodoFrenosDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HttpClientService clientService;
        VehicleService vehicleService;

        public VehiclesController(TodoFrenosDbContext context, UserManager<ApplicationUser> userManager, IConfiguration config, HttpClientService clientService)
        {
            _context = context;
            vehicleService = new VehicleService(config, clientService);
            _userManager = userManager;
        }

        // GET: Vehicles
        [Authorize(Roles = "Admin, Mecanico")]
        public async Task<IActionResult> Index()
        {
            var vehicles = await vehicleService.GetVehicles();
            var userIds = vehicles.Select(v => v.UserId).Distinct();
            var userEmails = new Dictionary<string, string>();

            foreach (var userId in userIds)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    userEmails[userId] = user.Email;
                }
            }

            ViewBag.UserEmails = userEmails;

            return View(vehicles);
        }

        // GET: Vehicles/Details/5
        [Authorize(Roles = "Admin, Mecanico ,User")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vehicle vehicle = await vehicleService.GetVehicle(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);
            ViewBag.CompleateName = $"{user.Nombre} {user.PrimApellido} {user.SegunApellido}";

            return View(vehicle);
        }

        // GET: Vehicles/Create
        [Authorize(Roles = "Admin, Mecanico, User")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Mecanico,User")]
        public async Task<IActionResult> Create(Vehicle vehicle)
        {
            var identidad = User.Identity as ClaimsIdentity;
            string idLogin = identidad.Claims.FirstOrDefault(m => m.Type == ClaimTypes.NameIdentifier).Value;
            vehicle.UserId = idLogin;

            if (ModelState.IsValid)
            {
                vehicle.CreationDate = DateTime.Now;
                vehicle.CarState = 1;

                try
                {
                    var resultado = await vehicleService.CreateVehicle(vehicle);

                    if (resultado != null)
                    {
                        TempData["MenasajeExito"] = "Se guardo el registro exitosamente";
                        return RedirectToAction(nameof(MyVehicles));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(vehicle);
                }
            }
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        [Authorize(Roles = "Admin, Mecanico")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vehicle vehicle = await vehicleService.GetVehicle(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Mecanico")]
        public async Task<IActionResult> Edit(long id, Vehicle vehicle)
        {
            if (id != vehicle.VehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var resultado = await vehicleService.EditVehicle(id, vehicle);

                    if (resultado != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(vehicle);
                }
            }
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        [Authorize(Roles = "Admin, Mecanico")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultado = await vehicleService.DeleteVehicle(id.Value);

            if (resultado)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Error al inactivar el vehiculo.");
            var vehicle = await vehicleService.GetVehicle(id);

            return View(vehicle);
        }

        [Authorize(Roles = "Admin, Mecanico")]
        public async Task<IActionResult> Activate(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultado = await vehicleService.ActivateVehicle(id.Value);

            if (resultado)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Error al activar el vehiculo.");
            var vehicle = await vehicleService.GetVehicle(id);

            return View(vehicle);
        }

        /* MyVehicles */
        [Authorize(Roles = "User, Mecanico, Admin")]
        public async Task<IActionResult> MyVehicles()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);
            ViewBag.CompleateName = $"{user.Nombre} {user.PrimApellido} {user.SegunApellido}";

            var result = await vehicleService.GetMyVehicles(userId);
            return View(result);
        }

        /* GET: Edit MyVehicles */
        [Authorize(Roles = "User, Mecanico, Admin")]
        public async Task<IActionResult> EditMyVehicles(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vehicle vehicle = await vehicleService.GetVehicle(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        /* POST: Edit MyVehicles */
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User, Mecanico, Admin")]
        public async Task<IActionResult> EditMyVehicles(long id, Vehicle vehicle)
        {
            if (id != vehicle.VehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var resultado = await vehicleService.EditVehicle(id, vehicle);

                    if (resultado != null)
                    {
                        return RedirectToAction(nameof(MyVehicles));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(vehicle);
                }
            }
            return View(vehicle);
        }

        [Authorize(Roles = "User, Mecanico, Admin")]
        public async Task<IActionResult> DeleteMyVehicles(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultado = await vehicleService.DeleteVehicle(id.Value);

            if (resultado)
            {
                return RedirectToAction(nameof(MyVehicles));
            }

            ModelState.AddModelError(string.Empty, "Error al inactivar el vehiculo.");
            var vehicle = await vehicleService.GetVehicle(id);

            return View(vehicle);
        }

        private bool VehicleExists(long id)
        {
            return _context.Vehicles.Any(e => e.VehicleId == id);
        }
    }
}
