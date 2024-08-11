using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using FrontEnd.ConsumoServices;

namespace FrontEnd.Controllers
{
    public class DetallesNominasController : Controller
    {
        private readonly TodoFrenosDbContext _context;
        DetallesNominasServices _detallesNominaServices;

        public DetallesNominasController(TodoFrenosDbContext context, IConfiguration config)
        {
            _context = context;
            _detallesNominaServices = new DetallesNominasServices(config);
        }

        // GET: DetallesNominas
        public async Task<IActionResult> Index()
        {
            
            var detallesNominas = await _context.DetallesNominas
            .Include(d => d.Empleado)  // Incluye los datos de la entidad Empleado
            .Include(d => d.Nomina)    // Incluye los datos de la entidad Nomina
                .ToListAsync();

            return View(detallesNominas);
        }
            // GET: DetallesNominas/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallesNomina = await _detallesNominaServices.GetDetallesNomina(id);
            if (detallesNomina == null)
            {
                return NotFound();
            }

            return View(detallesNomina);
        }

        // GET: DetallesNominas/Create
        public IActionResult Create()
        {
            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Nombre");
            ViewData["NominaId"] = new SelectList(_context.Nominas, "NominaId", "FechaCreacion");
            return View();
        }

        // POST: DetallesNominas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DetallesNomina detallesNomina)
        {
            if (ModelState.IsValid)
            {
                // Realizar cálculos antes de enviar
                RealizarCalculos(detallesNomina);

                var resultado = await _detallesNominaServices.CreateDetallesNomina(detallesNomina);
                if (resultado != null)
                {
                    TempData["MensajeExito"] = "Se agregó un nuevo detalle a la nómina exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Error"] = "No se puede crear por el estado de empleado o nomina se encuetra desactivado.";
                   return RedirectToAction("Create");
                    
                }
            }

            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Nombre", detallesNomina.EmpleadoId);
            ViewData["NominaId"] = new SelectList(_context.Nominas, "NominaId", "FechaCreacion", detallesNomina.NominaId);
            return View(detallesNomina);
        }

        // GET: DetallesNominas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallesNomina = await _detallesNominaServices.GetDetallesNomina(id);
            if (detallesNomina == null)
            {
                return NotFound();
            }

            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", detallesNomina.EmpleadoId);
            ViewData["NominaId"] = new SelectList(_context.Nominas, "NominaId", "NominaId", detallesNomina.NominaId);
            return View(detallesNomina);
        }

        // POST: DetallesNominas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DetallesNomina detallesNomina)
        {
            if (id != detallesNomina.DetalleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Realizar cálculos antes de enviar
                    RealizarCalculos(detallesNomina);

                    var resultado = await _detallesNominaServices.EditDetallesNomina(id, detallesNomina);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallesNominaExists(detallesNomina.DetalleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["EmpleadoId"] = new SelectList(_context.Empleados, "Id", "Id", detallesNomina.EmpleadoId);
            ViewData["NominaId"] = new SelectList(_context.Nominas, "NominaId", "NominaId", detallesNomina.NominaId);
            return View(detallesNomina);
        }

        // GET: DetallesNominas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultado = await _detallesNominaServices.DeleteDetallesNomina(id.Value);
            if (resultado)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Error al inactivar el detalle.");
            var detallesNomina = await _detallesNominaServices.GetDetallesNomina(id);
            return View(detallesNomina);
        }

        // GET: DetallesNominas/Activate/5
        public async Task<IActionResult> Activate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultado = await _detallesNominaServices.ActivateDetallesNomina(id.Value);
            if (resultado)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Error al activar el detalle.");
            var detallesNomina = await _detallesNominaServices.GetDetallesNomina(id);
            return View(detallesNomina);
        }

        private bool DetallesNominaExists(int id)
        {
            return _context.DetallesNominas.Any(e => e.DetalleId == id);
        }

        private void RealizarCalculos(DetallesNomina detallesNomina)
        {
            if (detallesNomina.CantidadHoras.HasValue && detallesNomina.Hora.HasValue)
            {
                detallesNomina.Diario = detallesNomina.CantidadHoras.Value * detallesNomina.Hora.Value;
                detallesNomina.Semanal = detallesNomina.Diario * 7;
                detallesNomina.Mensual = detallesNomina.Semanal * 4;
                detallesNomina.Pago = detallesNomina.Semanal;
            }

            if (detallesNomina.CantidadHorasExtra.HasValue && detallesNomina.HorasExtra.HasValue)
            {
                detallesNomina.Pago += detallesNomina.CantidadHorasExtra.Value * detallesNomina.HorasExtra.Value;
            }

            if (detallesNomina.Ccss.HasValue)
            {
                detallesNomina.Pago -= detallesNomina.Ccss.Value;
            }

            if (detallesNomina.Vales.HasValue)
            {
                detallesNomina.Pago -= detallesNomina.Vales.Value;
            }
        }
    }
}
