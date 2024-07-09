using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using ProyectoTodoFrenosWeb.ConsumoServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DAL;

namespace ProyectoTodoFrenosWeb.Controllers
{
    [AllowAnonymous]
    public class SuppliersController : Controller
    {
        private readonly TodoFrenosDbContext _context;
        SupplierService supplierService;
        private readonly UserManager<ApplicationUser> _userManager;

        public SuppliersController(TodoFrenosDbContext context, IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            supplierService = new SupplierService(config);
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var suppliers = await _context.Suppliers
                    .Where(s => s.SupplierState != -1) // Filtrar estados eliminados
                    .ToListAsync();

                return View(suppliers);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al cargar los proveedores: {ex.Message}";
                return View(); // Devuelve la vista con un mensaje de error
            }
        }

        // GET: Suppliers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            Suppliers supplier = await supplierService.GetSupplier(id);
            if (id == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Suppliers/Create")]
        public async Task<IActionResult> CreateSupplier([Bind("Email,SupplierCreationDate,Cause,SupplierState")] Suppliers suppliers)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(suppliers.Email);
                if (existingUser == null)
                {
                    ModelState.AddModelError("Email", "Este correo electrónico no está registrado como usuario.");
                    return View("Create", suppliers);
                }

                _context.Add(suppliers);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Proveedor registrado correctamente.";
                return RedirectToAction("Create");
            }

            return View("Create", suppliers);
        }


        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Suppliers supplier = await supplierService.GetSupplier(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Suppliers supplier)
        {
            if (id != supplier.SupplierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await supplierService.EditSupplier((long)id, supplier);

                    if (result != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.SupplierId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultado = await supplierService.DeleteSupplier(id.Value);

            if (resultado)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Error al inactivar el proveedor.");
            var supplier = await supplierService.GetSupplier(id);

            return View(supplier);
        }

        private bool SupplierExists(long id)
        {
            return _context.Suppliers.Any(e => e.SupplierId == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptSupplier(long id)
        {
            try
            {
                var result = await supplierService.AcceptSupplier(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Cita aceptada correctamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Hubo un problema al aceptar la cita.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hubo un error al procesar la solicitud: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectSupplier(long id)
        {
            try
            {
                var result = await supplierService.RejectSupplier(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Cita rechazada correctamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Hubo un problema al rechazar la cita.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hubo un error al procesar la solicitud: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public IActionResult GetPendingSuppliersCount()
        {
            int pendingState = 0; // Define el estado pendiente
            var pendingCount = _context.Suppliers.Count(s => s.SupplierState == pendingState);
            return Json(pendingCount);
        }
    }
}
