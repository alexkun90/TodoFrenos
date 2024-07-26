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
using DAL;
using Microsoft.AspNetCore.Identity;

namespace ProyectoTodoFrenosWeb.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly TodoFrenosDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        AppointmentService appointmentService;
        

        public AppointmentsController(TodoFrenosDbContext context, IConfiguration config, UserManager<ApplicationUser> _userManager)
        {
            _context = context;
            appointmentService = new AppointmentService(config);
            this._userManager = _userManager;
           
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            try
            {
                var appointments = await _context.Appointments
                    .Include(a => a.User) // Filtrar estados eliminados
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
            var userId =  _userManager.GetUserId(User);
            appointment.UserId = userId;
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
