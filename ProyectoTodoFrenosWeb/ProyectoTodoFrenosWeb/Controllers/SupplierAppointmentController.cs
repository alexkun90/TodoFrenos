using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoTodoFrenosWeb.ConsumoServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace ProyectoTodoFrenosWeb.Controllers
{
    public class SupplierAppointmentController : Controller
    {

        private readonly TodoFrenosDbContext _context;
        private readonly HttpClientService clientService;
        private readonly IEmailSender _emailSender;

        SupplierAppointmentService supplierAppointmentservice;
        public SupplierAppointmentController(TodoFrenosDbContext _context, IConfiguration config, HttpClientService clientService,
                                             IEmailSender _emailSender)
        {
            this._context = _context;
            supplierAppointmentservice = new SupplierAppointmentService(config, clientService);
            this._emailSender = _emailSender;
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
                var supplier = await supplierAppointmentservice.GetSupplierAppointment(id);
                var supplierList = await _context.SupplierLists
                    .FirstOrDefaultAsync(s => s.SupplierListId == supplier.SupplierListId);

                var result = await supplierAppointmentservice.AcceptSupplierAppointment(id);
                if (result)
                {
                    var fecha = supplier.AppointCreationDate.HasValue
                        ? supplier.AppointCreationDate.Value.ToString("dd/MM/yyyy")
                        : "Fecha no disponible";

                    var hora = supplier.AppointCreationDate.HasValue
                        ? supplier.AppointCreationDate.Value.ToString("HH:mm")
                        : "Hora no disponible";

                    var emailSubject = "Confirmación de Cita Aceptada - Taller Todo Frenos";
                    var emailMessage = $@"
                        <p>Estimado/a {supplierList.SupplierName},</p>
                        <p>Gracias por ponerse en contacto con el Taller Todo Frenos. Nos complace informarle que su cita ha sido aceptada con éxito.</p>
                        <p><strong>Detalles de la Cita:</strong></p>
                        <ul>
                            <li><strong>Fecha:</strong> {fecha}</li>
                            <li><strong>Hora:</strong> {hora}</li>
                            <li><strong>Ubicación:</strong> 100m Sur, 25m Oeste del Hospital Maternidad La Carit. Av. 26. Calle 8., San José, Costa Rica</li>
                        </ul>
                        <p>Si tiene alguna consulta adicional o necesita realizar algún cambio en la cita, no dude en contactarnos respondiendo a este correo o llamándonos al 2227 6448.</p>
                        <p>Estamos a su disposición y agradecemos su confianza en nuestros servicios.</p>
                        <p>Atentamente,<br/>El equipo de Todo Frenos</p>
                    ";
                    await _emailSender.SendEmailAsync(supplier.SupplierEmail, emailSubject, emailMessage);

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
                var supplier = await supplierAppointmentservice.GetSupplierAppointment(id);
                var supplierList = await _context.SupplierLists
                    .FirstOrDefaultAsync(s => s.SupplierListId == supplier.SupplierListId);
                var result = await supplierAppointmentservice.RejectSupplierAppointment(id);
                if (result)
                {
                    var emailSubject = "Respuesta a su Solicitud de Cita - Taller Todo Frenos";
                    var emailMessage = $@"
                        <p>Estimado/a {supplierList.SupplierName},</p>
                        <p>Agradecemos su interés en nuestros servicios y por contactarnos para programar una cita.</p>
                        <p>Lamentamos informarle que, debido a nuestra agenda actual y a los requerimientos específicos de nuestros clientes, no podemos confirmar su solicitud en este momento.</p>
                        <p>Valoramos enormemente a nuestros proveedores y entendemos la importancia de mantener una buena relación comercial.</p>
                        <p>Agradecemos su comprensión y esperamos poder trabajar juntos en el futuro.</p>
                        <p>Atentamente,<br/>El equipo de Todo Frenos</p>
                    ";
                    await _emailSender.SendEmailAsync(supplier.SupplierEmail, emailSubject, emailMessage);
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
