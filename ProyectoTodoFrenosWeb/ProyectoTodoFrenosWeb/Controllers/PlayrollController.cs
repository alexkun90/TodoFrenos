using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using ProyectoTodoFrenosWeb.ConsumoServices;

namespace ProyectoTodoFrenosWeb.Controllers
{
    public class PlayrollController : Controller
    {
        PlayrollService service;
        public PlayrollController(IConfiguration config)
        {
            service = new PlayrollService(config);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayrollDetail model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var resultado = await service.CreatePlayroll(model);

                    if (resultado != null)
                    {
                        TempData["MenasajeExito"] = "Nomina creada Exitosamente";
                        return RedirectToAction("Index","Employees");
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
