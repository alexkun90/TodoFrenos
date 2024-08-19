using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoTodoFrenosWeb.ConsumoServices;
using System.Security.Claims;

namespace ProyectoTodoFrenosWeb.Controllers
{
    [Authorize(Roles = "Admin, Mecanico")]
    public class EmployeesController : Controller
    {
        EmployeeService employeeService;
        public EmployeesController(IConfiguration config)
        {
            employeeService = new EmployeeService(config);
        }
        public async Task<IActionResult> Index()
        {
            var listEmployee = await employeeService.GetEmployee();
            return View(listEmployee);
        }

        public async Task<IActionResult> Details(long id)
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
    }
}
