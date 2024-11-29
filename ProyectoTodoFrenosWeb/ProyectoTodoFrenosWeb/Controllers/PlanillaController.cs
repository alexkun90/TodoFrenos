using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ProyectoTodoFrenosWeb.ConsumoServices;
using SelectPdf;

namespace ProyectoTodoFrenosWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PlanillaController : Controller
    {
        PlanillaService service;
        PlayrollService playrollService;
        private readonly HttpClientService clientService;
        private readonly ICompositeViewEngine _viewEngine;
        private readonly ITempDataProvider _tempDataProvider;

        public PlanillaController(IConfiguration config, HttpClientService clientService,
                                  ICompositeViewEngine viewEngine, ITempDataProvider tempDataProvider)
        {
            service = new PlanillaService(config, clientService);
            playrollService = new PlayrollService(config, clientService);
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
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
            var payroll = playrollService.GetPlayrollDetails(nominaId);
            DateTime? StartDate = payroll.Result.FechaInicio;
            DateTime? EndDate = payroll.Result.FechaFin;

            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

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
                        TempData["Message"] = "Ya existen 2 planillas asociadas a esta nómina. No se puede crear más.";
                    }
                }

                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            var payroll = await playrollService.GetPlayrollDetails(model.NominaId);
            ViewBag.StartDate = payroll?.FechaInicio;
            ViewBag.EndDate = payroll?.FechaFin;

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Mecanico,User")]
        public async Task<IActionResult> DownloadPdf(long planillaId)
        {

            var result = await service.GetPlanilla(planillaId);
            if (result == null)
            {
                return NotFound();
            }
            // Generar la vista HTML como cadena
            string htmlContent = await RenderViewAsStringAsync("Details", result);

            // Crear un convertidor de HTML a PDF
            var converter = new HtmlToPdf();

            // Convertir el HTML a PDF
            var pdfDocument = converter.ConvertHtmlString(htmlContent);

            // Enviar el PDF al navegador para descargarlo
            byte[] pdfBytes = pdfDocument.Save();
            pdfDocument.Close();

            return File(pdfBytes, "application/pdf", "Planilla Empleado.pdf");
        }


        private async Task<string> RenderViewAsStringAsync(string viewName, object model)
        {
            var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model
            };
            using (var writer = new StringWriter())
            {
                var viewResult = _viewEngine.FindView(ControllerContext, viewName, false);
                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"La vista '{viewName}' no fue encontrada.");
                }
                var viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    viewData,
                    new TempDataDictionary(ControllerContext.HttpContext, _tempDataProvider),
                    writer,
                    new HtmlHelperOptions()
                );
                await viewResult.View.RenderAsync(viewContext);
                return writer.GetStringBuilder().ToString();
            }
        }
    }
}
