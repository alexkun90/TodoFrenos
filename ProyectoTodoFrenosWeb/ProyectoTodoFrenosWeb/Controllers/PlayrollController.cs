using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using ProyectoTodoFrenosWeb.ConsumoServices;
using ProyectoTodoFrenosWeb.ViewModels;
using SelectPdf;
using System.Net.Http;
using System.Security.Claims;

namespace ProyectoTodoFrenosWeb.Controllers
{
    [Authorize(Roles = "Admin, Mecanico")]
    public class PlayrollController : Controller
    {
        PlayrollService service;
        RenderHTMLService renderHTMLService;

        public PlayrollController(IConfiguration config, HttpClientService clientService, RenderHTMLService renderHTMLService)
        {
            service = new PlayrollService(config, clientService);
            this.renderHTMLService = renderHTMLService;
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

        [HttpPost]
        [Authorize(Roles = "Admin, Mecanico, User")]
        public async Task<IActionResult> DownloadPdf(long nominaId)
        {
            var result = await service.GetPlayrollDetails(nominaId);
            if (result == null)
            {
                return NotFound();
            }
            // Generar la vista HTML como cadena
            string htmlContent = await  renderHTMLService.RenderViewAsStringAsync(ControllerContext,"Details",result);

            // Crear un convertidor de HTML a PDF
            var converter = new HtmlToPdf();

            // Configurar cabeceras y optimizaciones
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.WebPageWidth = 1024;
            converter.Options.WebPageHeight = 0;
            converter.Options.PdfCompressionLevel = PdfCompressionLevel.Best;


            // Convertir el HTML a PDF
            var pdfDocument = converter.ConvertHtmlString(htmlContent);

            // Enviar el PDF al navegador para descargarlo
            byte[] pdfBytes = pdfDocument.Save();
            pdfDocument.Close();

            // Configurar headers para la respuesta
            Response.Headers.Add("Cache-Control", "no-store");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");

            return File(pdfBytes, "application/pdf", "Nomina_Empleado.pdf");
        }



        //[HttpPost]
        //[Authorize(Roles = "Admin, Mecanico,User")]
        //public async Task<IActionResult> DownloadPdf(long nominaId)
        //{

        //    var result = await service.GetPlayrollDetails(nominaId);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    // Generar la vista HTML como cadena
        //    string htmlContent = await RenderViewAsStringAsync("Details", result);

        //    // Crear un convertidor de HTML a PDF
        //    var converter = new HtmlToPdf();

        //    // Convertir el HTML a PDF
        //    var pdfDocument = converter.ConvertHtmlString(htmlContent);

        //    // Enviar el PDF al navegador para descargarlo
        //    byte[] pdfBytes = pdfDocument.Save();
        //    pdfDocument.Close();

        //    return File(pdfBytes, "application/pdf", "Nómina Empleado.pdf");
        //}
    }
}
