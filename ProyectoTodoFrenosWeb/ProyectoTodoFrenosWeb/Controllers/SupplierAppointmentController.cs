using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoTodoFrenosWeb.ConsumoServices;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoTodoFrenosWeb.Controllers
{
    public class SupplierAppointmentController : Controller
    {

        private readonly TodoFrenosDbContext _context;
        
        SupplierAppointmentService supplierAppointmentservice;
        public SupplierAppointmentController(TodoFrenosDbContext _context, IConfiguration config)
        {
            this._context = _context;
            supplierAppointmentservice = new SupplierAppointmentService(config);
        }

        [Authorize(Roles = "Admin, Mecanico")]
        public async Task<IActionResult> Index()
        {
            var result = await supplierAppointmentservice.GetSupplierAppointments();
            return View(result);
        }

        [Authorize(Roles = "Admin, Mecanico")]
        // GET: SupplierAppointmentController/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var result = await supplierAppointmentservice.GetSupplierAppointment(id);
            return View(result);
        }

        // GET: SupplierAppointmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupplierAppointmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierAppointment supplierAppointment)
        {
            var supplierList = await _context.SupplierLists
                    .FirstOrDefaultAsync(s => s.SupplierEmail == supplierAppointment.SupplierEmail);

            if (supplierList == null)
            {
                ModelState.AddModelError("Email", "El correo electrónico no está registrado en la lista de proveedores.");
                TempData["ErrorMessage"] = "El correo electrónico no está registrado en la lista de proveedores.";
                return View(supplierAppointment);
            }

            supplierAppointment.SupplierListId = supplierList.SupplierListId;

            if (ModelState.IsValid)
            {
                try
                {
                    var resultado = await supplierAppointmentservice.CreateSupplierAppointment(supplierAppointment);

                    TempData["SuccessMessage"] = "Solicitud de cita enviada correctamente.";
                    return RedirectToAction(nameof(Create));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "No se pudo registrar la cita. Por favor, inténtelo de nuevo.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "La información del formulario no es válida. Por favor, revise los errores.";
            }

            return View(supplierAppointment);
        }


        // GET: SupplierAppointmentController/Delete/5
        [Authorize(Roles = "Admin, Mecanico")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultado = await supplierAppointmentservice.DeleteSupplierAppointment(id.Value);

            if (resultado)
            {
                return RedirectToAction(nameof(Create));
            }

            ModelState.AddModelError(string.Empty, "Error al inactivar la cita.");
            var supplierAppointment = await supplierAppointmentservice.GetSupplierAppointment(id);

            return View(supplierAppointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Mecanico")]
        public async Task<IActionResult> AcceptSupplierAppointment(long id)
        {
            try
            {
                var result = await supplierAppointmentservice.AcceptSupplierAppointment(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Cita aceptada correctamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Hubo un problema al aceptar la cita.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hubo un error al procesar la solicitud: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Mecanico")]
        public async Task<IActionResult> RejectSupplierAppointment(long id)
        {
            try
            {
                var result = await supplierAppointmentservice.RejectSupplierAppointment(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Cita rechazada correctamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Hubo un problema al rechazar la cita.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hubo un error al procesar la solicitud: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
