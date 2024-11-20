using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoTodoFrenosWeb.ConsumoServices;
using ProyectoTodoFrenosWeb.ViewModels;
using System.Net.Http;

namespace ProyectoTodoFrenosWeb.Controllers
{
    [Authorize(Roles = "Admin, Mecanico")]
    public class PlayrollController : Controller
    {
        PlayrollService service;
        EmployeeService employeeService;

        private readonly HttpClientService clientService;
        public PlayrollController(IConfiguration config, HttpClientService clientService)
        {
            service = new PlayrollService(config, clientService);
            employeeService = new EmployeeService(config, clientService);
        }

        public async Task<IActionResult> Index()
        {
            var list = await service.GetAllPlayrolls();
            return View(list);
        }

        public async Task<IActionResult> Details(long nominaId)
        {
            PlayRollDTO nomina = await service.GetPlayrollDetails(nominaId);
            if (nominaId == null)
            {
                return NotFound();
            }
            return View(nomina);
        }

        public IActionResult Create(long empleadoid)
        {
            var model = new PlayrollDetail
            {
                EmployeeId = empleadoid,
                SalarioBruto = 0
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayrollDetail model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var resultado = await service.CreatePlayroll(model.EmployeeId, model);

                    if (resultado != null)
                    {
                        TempData["MenasajeExito"] = "Nomina creada Exitosamente";
                        return RedirectToAction("Index", "Playroll");
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
