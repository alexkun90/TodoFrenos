using DAL.Models;
using FrontEnd.ConsumoServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoTodoFrenosWeb.ConsumoServices;
using System.Security.Claims;

namespace ProyectoTodoFrenosWeb.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly TodoFrenosDbContext _context;

        EmployeeService employeeService;
        public EmployeesController(TodoFrenosDbContext context, IConfiguration config)
        {
            _context = context;
            employeeService = new EmployeeService(config);
        }
        public async Task<IActionResult> Index()
        {
            var listEmployee = await employeeService.GetEmployee();
            return View(listEmployee);
        }

        public async Task<IActionResult> Details(long? id)
        {
            Employee employee = await employeeService.GetEmployee(id);
            if(id == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee model)
        {
            if (ModelState.IsValid)
            {
                
                try
                {
                    model.EstadoEmpleado = true;
                    var resultado = await employeeService.CreateEmployee(model);

                    if (resultado != null)
                    {
                        TempData["MenasajeExito"] = "Se guardo el registro exitosamente";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View(model);
                    }
                }

                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(model);
                }
            }
            return View(model);
        }


        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee empleado = await employeeService.GetEmployee(id);

            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Employee empleado)
        {
            if (id != empleado.EmpleadoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var resultado = await employeeService.EditEmployee((long)id, empleado);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(empleado.EmpleadoId))
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

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultado = await employeeService.DeleteEmployee(id.Value);

            if (resultado)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Error al inactivar el empleado.");
            var empleado = await employeeService.GetEmployee(id);


            return View(empleado);
        }

        // GET: Employee/Activate/5
        public async Task<IActionResult> Activate(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultado = await employeeService.ActivateEmployee(id.Value);

            if (resultado)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Error al activar el empleado.");
            var empleado = await employeeService.GetEmployee(id);


            return View(empleado);
        }

        private bool EmployeeExists(long id)
        {
            return _context.Employees.Any(e => e.EmpleadoId == id);
        }

    }
}
