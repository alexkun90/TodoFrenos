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
using System.Drawing;
using Microsoft.AspNetCore.Identity;
using DAL;

namespace ProyectoTodoFrenosWeb.Controllers
{
    [Authorize]
    //[Authorize(Roles = "User,Mecanico")]
    public class VehiclesController : Controller
    {
        private readonly TodoFrenosDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        VehicleService vehicleService;

        public VehiclesController(TodoFrenosDbContext context, UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _context = context;
            vehicleService = new VehicleService(config);
            _userManager = userManager;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var emailUser = User.FindFirst(ClaimTypes.Email).Value;
            ViewBag.Email = emailUser.ToString();
            
            var vehicles = await vehicleService.GetVehicles();

            return View(vehicles);
        }       

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            Vehicle vehicle = await vehicleService.GetVehicle(id);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);
            var name = user.Nombre;
            var surname = user.PrimApellido;
            var surname2 = user.SegunApellido;

            ViewBag.CompleateName = name + " " + surname + " " + surname2;

            if (id == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vehicle vehicle)
        {
            var identidad = User.Identity as ClaimsIdentity;
            string idLogin = identidad.Claims.FirstOrDefault(m => m.Type == ClaimTypes.NameIdentifier).Value;
            vehicle.UserId = idLogin;

            if (ModelState.IsValid)
            {
                vehicle.CreationDate = DateTime.Now;
                
                try
                {
                    var resultado = await vehicleService.CreateVehicle(vehicle);

                    if (resultado != null)
                    {
                        TempData["MenasajeExito"] = "Se guardo el registro exitosamente";
                        return RedirectToAction(nameof(MyVehicles));
                    }
                }
                
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(vehicle);
                }
            }
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
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
                    try
                    {
                        var resultado = await vehicleService.EditVehicle((long)id, vehicle);

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
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.VehicleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }


            }
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
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

        /*MyVehicles*/

        public async Task<IActionResult> MyVehicles()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);
            var name = user.Nombre;
            var surname = user.PrimApellido;
            var surname2 = user.SegunApellido;

            ViewBag.CompleateName = name + " " + surname + " " + surname2;
            var result = await vehicleService.GetMyVehicles(userId);

            return View(result);

        }

        /*GetEdit MyVehicles*/
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

        /*PostEdit MyVehicles*/
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    try
                    {
                        var resultado = await vehicleService.EditVehicle((long)id, vehicle);

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
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.VehicleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }


            }
            return View(vehicle);
        }

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
