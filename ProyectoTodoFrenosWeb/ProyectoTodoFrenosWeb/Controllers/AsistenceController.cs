using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoTodoFrenosWeb.ConsumoServices;

namespace ProyectoTodoFrenosWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AsistenceController : Controller
    {

        AsistenceService service;
        EmployeeService employeeService;
        public AsistenceController(IConfiguration config)
        {
            this.service = new AsistenceService(config);
            this.employeeService = new EmployeeService(config);
        }
        public async Task<IActionResult> Index(long id)
        {
            Employee employeeId = await employeeService.GetEmployee(id);
            ViewBag.EmployeeId = employeeId.EmpleadoId.ToString();
            var result = await service.GetList(id);
            return View(result);
        }

        public async Task<IActionResult> Details(long id)
        {
            Asistence asistence = await service.GetAsistence(id);
            if (id == null)
            {
                return NotFound();
            }
            return View(asistence);
        }

        public IActionResult Create(long empleadoId)
        {
            var asistence = new Asistence { EmpleadoId =  empleadoId };
            return View(asistence);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Asistence model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await service.CreateAsistence(model);
                    if (result != null)
                    {
                        return RedirectToAction(nameof(Index), new { id = model.EmpleadoId });
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
            return View(model);
        }
    }
}
