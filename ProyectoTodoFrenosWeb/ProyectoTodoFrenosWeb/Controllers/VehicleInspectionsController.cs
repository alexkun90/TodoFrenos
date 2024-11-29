using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using ProyectoTodoFrenosWeb.ConsumoServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using DAL;
using SelectPdf;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ProyectoTodoFrenosWeb.Controllers
{
    //[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0, VaryByQueryKeys = new[] { "*" })]
    [Authorize]
    public class VehicleInspectionsController : Controller
    {
        VehicleInspectionService service;
        VehicleService serviceVehicle;
        private readonly HttpClientService clientService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICompositeViewEngine _viewEngine;
        private readonly ITempDataProvider _tempDataProvider;

        public VehicleInspectionsController(TodoFrenosDbContext context, UserManager<ApplicationUser> userManager,
                                            IConfiguration config, HttpClientService clientService,
                                            ICompositeViewEngine viewEngine, ITempDataProvider tempDataProvider)
        {
            this.service = new VehicleInspectionService(config, clientService);
            this.serviceVehicle = new VehicleService(config, clientService);
            _userManager = userManager;
            _viewEngine = viewEngine; 
            _tempDataProvider = tempDataProvider;
        }

        // GET: VehicleInspections
        [Authorize(Roles = "Admin, Mecanico,User")]
        public async Task<IActionResult> Index(long? id)
        {
            Vehicle vehiclePlate = await serviceVehicle.GetVehicle(id);
            ViewBag.VehiclePlate = vehiclePlate.Plate.ToString();
            ViewBag.VehicleId = id;
            
            var result = await service.GetList(id);
            return View(result);
        }

        // GET: VehicleInspections/Details/5
        [Authorize(Roles = "Admin, Mecanico,User")]
        public async Task<IActionResult> Details(long? id)
        {
            VehicleInspection inspection = await service.GetVehicleInspection(id);
            if (id == null)
            {
                return NotFound();
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRoles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(userId));
            var vehicleId = inspection.VehicleId;
            var vehicle = await serviceVehicle.GetVehicle(vehicleId);
            if (userRoles.Contains("User") && vehicle.UserId != userId) // Suponiendo que `Vehicle` tiene una propiedad `UserId`
            {
                return Forbid(); // Deniega el acceso si no es propietario del vehículo
            }

            return View(inspection);
        }

        [Authorize(Roles = "Admin,Mecanico")]
        // GET: VehicleInspections/Create
        [Authorize(Roles = "Admin, Mecanico")]
        public IActionResult Create(long vehicleId)
        {
            var inspection = new VehicleInspection { VehicleId = vehicleId };
            return View(inspection);
        }

        // POST: VehicleInspections/Create
        [Authorize(Roles = "Admin,Mecanico")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Mecanico")]
        public async Task<IActionResult> Create(VehicleInspection vehicleInspection)
        {
            if (ModelState.IsValid)
            {
                vehicleInspection.InspectionDate = DateTime.Now;
                try
                {
                    var result = await service.CreateVehicleInspection(vehicleInspection);
                    if (result != null)
                    {
                        return RedirectToAction(nameof(Index), new { id = vehicleInspection.VehicleId });
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
            return View(vehicleInspection);
        }

        // GET: VehicleInspections/Edit/5
        [Authorize(Roles = "Admin, Mecanico")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VehicleInspection vehicleInspection = await service.GetVehicleInspection(id);
            
            if (vehicleInspection == null)
            {
                return NotFound();
            }
            //ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "Plate", vehicleInspection.VehicleId);
            return View(vehicleInspection);
        }

        // POST: VehicleInspections/Edit/5
        [Authorize(Roles = "Admin,Mecanico")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Mecanico")]
        public async Task<IActionResult> Edit(long id, VehicleInspection vehicleInspection)
        {
            if (id != vehicleInspection.VehicleInspectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(vehicleInspection.OilChange == 0)
                    {
                        vehicleInspection.DatePerformed = null;
                        vehicleInspection.OilChangeKilometraje = null;
                    }

                    var result = await service.EditVehicleInspection((long)id,vehicleInspection);
                    if (result != null)
                    {
                        return RedirectToAction(nameof(Index), new { id = vehicleInspection.VehicleId });
                    }
                }
                catch (Exception)
                {          
                    throw;   
                }
            }
            return View(vehicleInspection);
        }

        // GET: VehicleInspections/Delete/5
        [Authorize(Roles = "Admin, Mecanico")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleInspection = await service.GetVehicleInspection(id);
            if (vehicleInspection == null)
            {
                return NotFound();
            }
            return View(vehicleInspection);
        }

        // POST: VehicleInspections/Delete/5
        [Authorize(Roles = "Admin,Mecanico")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Mecanico")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            VehicleInspection vehicleInspection1 = await service.GetVehicleInspection(id);
            var result = await service.DeleteVehicleInspection(id);
            if (result )
            {
                return RedirectToAction(nameof(Index), new { id = vehicleInspection1.VehicleId });
            }

            ModelState.AddModelError(string.Empty, "Error deleting vehicle. Please try again.");
            var vehicleInspection = await service.GetVehicleInspection(id);
            return View("Delete", vehicleInspection);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Mecanico,User")]
        public async Task<IActionResult> DownloadPdf(long id)
        {
            VehicleInspection inspection = await service.GetVehicleInspection(id);
            if (inspection == null)
            {
                return NotFound();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRoles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(userId));
            var vehicleId = inspection.VehicleId;
            var vehicle = await serviceVehicle.GetVehicle(vehicleId);
            if (userRoles.Contains("User") && vehicle.UserId != userId) // Suponiendo que `Vehicle` tiene una propiedad `UserId`
            {
                return Forbid(); // Deniega el acceso si no es propietario del vehículo
            }

            // Generar la vista HTML como cadena
            string htmlContent = await RenderViewAsStringAsync("Details", inspection);

            string additionalHtml = @"
                <div style='text-align: center; margin-top: 5%;'> 
                    <img src='https://th.bing.com/th/id/R.4e082d1c06a52c8e9b8b7dcc8fae1a4d?rik=CJt0PbhK%2fM%2fI%2bg&riu=http%3a%2f%2fwww.todofrenoscr.com%2fimages%2flogo-index.png&ehk=be5cqlD85FVdl9L90gnyf1HlHzoNFY%2fh0ZpAPqsLNlk%3d&risl=&pid=ImgRaw&r=0' style='max-width: 200px; margin-bottom: 20px;' />
                    <h1 style='color: #000;'>Reporte de Inspección Vehicular</h1> 
                </div>"; 
            
            // Combinar el HTML adicional con el contenido renderizado de la vista
            string completeHtmlContent = additionalHtml + htmlContent;

            // Crear un convertidor de HTML a PDF
            var converter = new HtmlToPdf();

            // Convertir el HTML a PDF
            var pdfDocument = converter.ConvertHtmlString(completeHtmlContent);

            // Enviar el PDF al navegador para descargarlo
            byte[] pdfBytes = pdfDocument.Save();
            pdfDocument.Close();

            return File(pdfBytes, "application/pdf", "Inspección Vehícular Todo Frenos.pdf");
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
