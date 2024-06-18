using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using ProyectoTodoFrenosWeb.ConsumoServices;

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
            var appointments = await appointmentService.GetAppointments();
            return View(appointments);
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
        public async Task<IActionResult> Create( Appointment appointment)
        {
            var result = await appointmentService.CreateAppointment(appointment);
            if (result != null)
            {

                return RedirectToAction(nameof(Index));
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

    }
}
