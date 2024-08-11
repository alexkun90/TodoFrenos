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
    public class NominasController : Controller
    {
        private readonly TodoFrenosDbContext _context;
        
        NominaService nominaService;

        public NominasController(TodoFrenosDbContext context, IConfiguration config)
        {
            _context = context;
            nominaService = new NominaService(config);

        }

        // GET: Nominas
        public async Task<IActionResult> Index()
        {
            var nominas = await nominaService.GetNomina();
            return View(nominas);
        }

        // GET: Nominas/Details/5
        [Route("/Nomina/Detalles/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            Nomina nomina = await nominaService.GetNomina(id);

            if (id == null)
            {
                return NotFound();
            }

            return View(nomina);
        }

        public ActionResult CalcularTotal()
        {
            // Prepara el SelectList para el DropDown
            ViewData["NominaId"] = new SelectList(_context.Nominas, "NominaId", "FechaCreacion");

            // Envía un modelo vacío inicialmente
            return View(new Nomina());
        }

        [HttpPost]
        public async Task<IActionResult> CalcularTotal(int nominaId)
        {
            try
            {
                var nomina = await nominaService.CalcularTotal(nominaId);

                if (nomina == null)
                {
                    TempData["Error"] = "No se pudo calcular el total de la nómina porque la nómina no fue encontrada o está inactiva.";
                    return RedirectToAction("Index"); // Redireccionar al index en caso de error
                }

                TempData["MensajeExito1"] = "Se calculó el total de la nómina exitosamente.";
            }
            catch (Exception ex)
            {
                // Aquí se podría capturar y registrar el error real para propósitos de depuración
                TempData["Error"] = $"Ocurrió un error al calcular el total de la nómina: {ex.Message}";
            }

            // Redirigir al Index de Nomina después de intentar calcular
            return RedirectToAction("Index", "Nominas");
        }


        // GET: Nominas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nominas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Nomina nomina)
        {
            if (ModelState.IsValid)
            {
                nomina.FechaCreacion = DateTime.Now;
                nomina.Estado = true;

                var resultado = await nominaService.CreateNomina(nomina);

                if (resultado != null)
                {
                    TempData["MenasajeExito"] = "Se agrego una nueva nomina exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(nomina);
                }
            }
            return View(nomina);
        }

        // GET: Nominas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Nomina nomina = await nominaService.GetNomina(id);

            if (nomina == null)
            {
                return NotFound();
            }
            return View(nomina);
        }

        // POST: Nominas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Nomina nomina)
        {
            if (id != nomina.NominaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var resultado = await nominaService.EditNomina((int)id, nomina);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NominaExists(nomina.NominaId))
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
            return View(nomina);
        }


        // GET: Nominas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultado = await nominaService.DeleteNomina(id.Value);

            if (resultado)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Error al inactivar el nomina.");
            var nomina = await nominaService.GetNomina(id);


            return View(nomina);
        }

        // GET: Nominas/Activate/5
        public async Task<IActionResult> Activate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultado = await nominaService.ActivateNomina(id.Value);

            if (resultado)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Error al activar el nomina.");
            var nomina = await nominaService.GetNomina(id);


            return View(nomina);
        }

        private bool NominaExists(int id)
        {
            return _context.Nominas.Any(e => e.NominaId == id);
        }
    }
}
