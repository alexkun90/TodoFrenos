using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using ProyectoTodoFrenosWeb.ConsumoServices;

namespace ProyectoTodoFrenosWeb.Controllers
{
    public class PlanillaController : Controller
    {
        PlanillaService service;
        PlayrollService playrollService;
        public PlanillaController(IConfiguration config)
        {
            service = new PlanillaService(config);
            playrollService = new PlayrollService(config);
        }

        public async Task<IActionResult> Index(long nominaId)
        {
            var list = await service.GetAllPlanilla(nominaId);
            return View(list);
        }

        public async Task<IActionResult> Details(long planillaId)
        {
            var list = await service.GetPlanilla(planillaId);
            return View(list);
        }

        public IActionResult Create(long nominaId)
        {
            var model = new PlanillaEmpleado
            {
                NominaId = nominaId,
                SalarioBruto = 0,
                SEM = 0,
                IVM = 0,
                LPT = 0,
                ImpuestoRenta = 0,
                TotalDeducciones = 0,
                SalarioNetoFinal = 0
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlanillaEmpleado model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var resultado = await service.CreatePlanilla(model.NominaId, model);

                    if (resultado != null)
                    {
                        TempData["MenasajeExito"] = "Planilla creada Exitosamente";
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
