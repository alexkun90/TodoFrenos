﻿using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using ProyectoTodoFrenosWeb.ConsumoServices;

namespace ProyectoTodoFrenosWeb.Controllers
{
    public class PlayrollController : Controller
    {
        PlayrollService service;
        EmployeeService employeeService;
        public PlayrollController(IConfiguration config)
        {
            service = new PlayrollService(config);
            employeeService = new EmployeeService(config);
        }
        public IActionResult Create(long empleadoid)
        {
            var model = new PlayrollDetail { EmployeeId = empleadoid,
                                            SalarioBruto = 0};
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
