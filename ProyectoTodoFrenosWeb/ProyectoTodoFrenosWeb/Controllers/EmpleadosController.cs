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
    public class EmpleadosController : Controller
    {
        private readonly TodoFrenosDbContext _context;

        EmpleadoServices empleadoServices;

        public EmpleadosController(TodoFrenosDbContext context, IConfiguration config)
        {
            _context = context;
            empleadoServices = new EmpleadoServices(config);
        }

        // GET: Empleados
        public async Task<IActionResult> Index()
        {
            var empleado = await empleadoServices.GetEmpleado();
            return View(empleado);
        }

        // GET: Empleados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Empleado empleado = await empleadoServices.GetEmpleado(id);

            if (id == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                empleado.Estado = true;
                var resultado = await empleadoServices.CreateEmpleado(empleado);
                if (resultado != null)
                {
                    TempData["MenasajeExito"] = "Se agrego un nuevo detalle a la nomina exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(empleado);
                }
            }
            return View(empleado);
        }

        // GET: Empleados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Empleado empleado = await empleadoServices.GetEmpleado(id);

            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var resultado = await empleadoServices.EditEmpleado((int)id, empleado);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.Id))
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
            return View(empleado);
        }

        // GET: Empleado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultado = await empleadoServices.DeleteEmpleado(id.Value);

            if (resultado)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Error al inactivar el empleado.");
            var empleado = await empleadoServices.GetEmpleado(id);


            return View(empleado);
        }

        // GET: Empleado/Activate/5
        public async Task<IActionResult> Activate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultado = await empleadoServices.ActivateEmpleado(id.Value);

            if (resultado)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Error al activar el empleado.");
            var empleado = await empleadoServices.GetEmpleado(id);


            return View(empleado);
        }

        private bool EmpleadoExists(int id)
        {
            return _context.Empleados.Any(e => e.Id == id);
        }
    }
}
