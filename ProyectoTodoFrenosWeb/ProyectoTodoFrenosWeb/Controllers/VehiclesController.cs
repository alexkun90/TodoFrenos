﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using ProyectoTodoFrenosWeb.ConsumoServices;

namespace ProyectoTodoFrenosWeb.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly TodoFrenosDbContext _context;

        VehicleService vehicleService;

        public VehiclesController(TodoFrenosDbContext context,IConfiguration config)
        {
            _context = context;
            vehicleService = new VehicleService(config);

        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var vehicles = await vehicleService.GetVehicles();

            return View(vehicles);
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            Vehicle vehicle = await vehicleService.GetVehicle(id);

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
            if (ModelState.IsValid)
            {
                vehicle.CreationDate = DateTime.Now;

                try
                {
                    var resultado = await vehicleService.CreateVehicle(vehicle);

                    if (resultado != null)
                    {
                        TempData["MenasajeExito"] = "Se guardo el registro exitosamente";
                        return RedirectToAction(nameof(Index));
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

        /*if (id == null)
        {
            return NotFound();
        }

        var vehicle = await vehicleService.GetVehicle(id);

        if (vehicle == null)
        {
            return NotFound();
        }

        return View(vehicle);
    }

    // POST: Vehicles/Delete/5
    /*[HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        var resultado = await vehicleService.DeleteVehicle(id);

        if (resultado)
        {
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError(string.Empty, "Error deleting vehicle. Please try again.");
        var vehicle = await vehicleService.GetVehicle(id);

        return View("Delete", vehicle);
    }*/

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

        private bool VehicleExists(long id)
        {
            return _context.Vehicles.Any(e => e.VehicleId == id);
        }
    }
}
