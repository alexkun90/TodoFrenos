using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using ProyectoTodoFrenosWeb.ConsumoServices;
using System.Security.Claims;

namespace ProyectoTodoFrenosWeb.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly TodoFrenosDbContext _context;
        AppointmentService appointmentService;

        
        public AppointmentsController(TodoFrenosDbContext context, IConfiguration config)
        {
            _context = context;
            appointmentService = new AppointmentService(config);
           
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            try
            {
                var appointments = await _context.Appointments
                    .Include(a => a.User)
                    .Where(a => a.AppointState != -1) // Filtrar estados eliminados
                    .ToListAsync();

                return View(appointments);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al cargar las citas: {ex.Message}";
                return View(); // Devuelve la vista con un mensaje de error
            }
        }


        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            Appointment appointment = await appointmentService.GetAppointment(id);
            if (id == null)
            {
                return NotFound();
            }



            return View(appointment);
        }


        public IActionResult Create()
        {

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var resultado = await appointmentService.CreateAppointment(appointment);

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
                TempData["ErrorMessage"] = "Necesita iniciar sesión para agendar una cita";
            }

            return View(appointment);
        }



        public async Task<IActionResult> Edit(long? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            Appointment appointment = await appointmentService.GetAppointment(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id,  Appointment appointment)
        {
            if (id != appointment.AppointId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await appointmentService.EditAppointment((long)id, appointment);

                    if (result != null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }

            return View(appointment);
        }


        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultado = await appointmentService.DeleteAppointment(id.Value);

            if (resultado)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Error al inactivar la cita.");
            var appointment = await appointmentService.GetAppointment(id);

            return View(appointment);
        }

        /*
          [HttpPost, ActionName("Delete")]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> DeleteConfirmed(long id)
          {
              var appointment = await _context.Appointments.FindAsync(id);
              if (appointment != null)
              {
                  _context.Appointments.Remove(appointment);
              }

              await _context.SaveChangesAsync();
              return RedirectToAction(nameof(Index));
          }
         */

        private bool AppointmentExists(long id)
        {
            return _context.Appointments.Any(e => e.AppointId == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptAppointment(long id)
        {
            try
            {
                var result = await appointmentService.AcceptAppointment(id);
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
        public async Task<IActionResult> RejectAppointment(long id)
        {
            try
            {
                var result = await appointmentService.RejectAppointment(id);
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
        /*
        [HttpGet]
        public IActionResult GetPendingAppointmentsCount()
        {
            int pendingState = 0;
            5107var pendingCount = _context.Appointments.Count(a => a.AppointState == pendingState);
            return Json(pendingCount);
        }*/



    }
}
