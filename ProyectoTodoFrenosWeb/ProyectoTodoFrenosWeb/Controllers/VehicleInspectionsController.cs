using System;
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
    //[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0, VaryByQueryKeys = new[] { "*" })]
    public class VehicleInspectionsController : Controller
    {
        private readonly TodoFrenosDbContext _context;

        VehicleInspectionService service;
        VehicleService serviceVehicle;

        public VehicleInspectionsController(TodoFrenosDbContext context, IConfiguration config)
        {
            _context = context;
            this.service = new VehicleInspectionService(config);
            this.serviceVehicle = new VehicleService(config);
        }

        // GET: VehicleInspections
        public async Task<IActionResult> Index(long? id)
        {
            Vehicle vehiclePlate = await serviceVehicle.GetVehicle(id);
            ViewBag.VehiclePlate = vehiclePlate.Plate.ToString();
            ViewBag.VehicleId = id;
            
            var result = await service.GetList(id);
            return View(result);
        }

        // GET: VehicleInspections/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            VehicleInspection inspection = await service.GetVehicleInspection(id);
            if(id == null)
            {
                return NotFound();
            }
            return View(inspection);
        }

        // GET: VehicleInspections/Create
        public IActionResult Create(long vehicleId)
        {
            var inspection = new VehicleInspection { VehicleId = vehicleId };
            return View(inspection);
        }

        // POST: VehicleInspections/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleInspection vehicleInspection)
        {
            if (ModelState.IsValid)
            {
                vehicleInspection.InspectionDate = DateTime.Now;
                try
                {
                    var result = await service.CreateVehicleInspection(vehicleInspection);
                    if (result != null)
                    {
                        return RedirectToAction(nameof(Index), new { id = vehicleInspection.VehicleId });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Error creating inspection. Please try again.");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Error creating inspection. Please try again.");
                }
            }
            return View(vehicleInspection);
        }

        // GET: VehicleInspections/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VehicleInspection vehicleInspection = await service.GetVehicleInspection(id);
            
            if (vehicleInspection == null)
            {
                return NotFound();
            }
            //ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "Plate", vehicleInspection.VehicleId);
            return View(vehicleInspection);
        }

        // POST: VehicleInspections/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, VehicleInspection vehicleInspection)
        {
            if (id != vehicleInspection.VehicleInspectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(vehicleInspection.OilChange == 0)
                    {
                        vehicleInspection.DatePerformed = null;
                        vehicleInspection.NextChangeDue = null;
                    }

                    var result = await service.EditVehicleInspection((long)id,vehicleInspection);
                    if (result != null)
                    {
                        return RedirectToAction(nameof(Index), new { id = vehicleInspection.VehicleId });
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleInspectionExists(vehicleInspection.VehicleInspectionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            //ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "Plate", vehicleInspection.VehicleId);
            return View(vehicleInspection);
        }

        // GET: VehicleInspections/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleInspection = await service.GetVehicleInspection(id);
            if (vehicleInspection == null)
            {
                return NotFound();
            }
            return View(vehicleInspection);
        }

        // POST: VehicleInspections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            VehicleInspection vehicleInspection1 = await service.GetVehicleInspection(id);
            var result = await service.DeleteVehicleInspection(id);
            if (result )
            {
                return RedirectToAction(nameof(Index), new { id = vehicleInspection1.VehicleId });
            }

            ModelState.AddModelError(string.Empty, "Error deleting vehicle. Please try again.");
            var vehicleInspection = await service.GetVehicleInspection(id);
            return View("Delete", vehicleInspection);
        }

        private bool VehicleInspectionExists(long id)
        {
            return _context.VehicleInspections.Any(e => e.VehicleInspectionId == id);
        }
    }
}
