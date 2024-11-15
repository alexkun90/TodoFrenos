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
using Microsoft.AspNetCore.Authorization; // Importa el namespace para la autorización

namespace ProyectoTodoFrenosWeb.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly TodoFrenosDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HttpClientService clientService;
        AppointmentService appointmentService;


        public AppointmentsController(TodoFrenosDbContext context, IConfiguration config,UserManager<ApplicationUser> _userManager, HttpClientService clientService)
        {
            _context = context;
            appointmentService = new AppointmentService(config, clientService);
            this._userManager = _userManager;
        }

        // GET: Appointments
        [Authorize(Roles = "Admin, Mecanico")] 
        public async Task<IActionResult> Index()
        {
            try
            {
                var appointmentsList = await appointmentService.GetAppointments();
                return View(appointmentsList);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al cargar las citas: {ex.Message}";
                return View();
            }
        }

        // GET: Appointments/Details/5
        [Authorize(Roles = "Admin, Mecanico")] 
        public async Task<IActionResult> Details(long? id)
        {
            Appointment appointment = await appointmentService.GetAppointment(id);
            if (id == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        [Authorize(Roles = "User")] 
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")] 
        public async Task<IActionResult> Create(Appointment appointment)
        {
            var userId = _userManager.GetUserId(User);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Mecanico")] 
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
        [Authorize(Roles = "Admin, Mecanico")] 
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")] 
        public async Task<IActionResult> CancelAppointment(long id)
        {
            try
            {
                var result = await appointmentService.CancelAppointment(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Cita cancelada correctamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Hubo un problema al cancelar la cita.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hubo un error al procesar la solicitud: {ex.Message}";
            }

            return RedirectToAction(nameof(MyAppointments));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> PapeleraAppointment(long id)
        {
            try
            {
                var result = await appointmentService.PapeleraAppointment(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Cita enviada a la papelera.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Hubo un problema al enviada la cita a la papelera.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hubo un error al procesar la solicitud: {ex.Message}";
            }

            return RedirectToAction(nameof(MyAppointments));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ReturnAppointment(long id)
        {
            try
            {
                var result = await appointmentService.ReturnAppointment(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Cita enviada a la lista de citas del usuario.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Hubo un problema al enviada la cita a la lista de citas del usuario.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Hubo un error al procesar la solicitud: {ex.Message}";
            }

            return RedirectToAction(nameof(MyPaper));
        }

        [Authorize(Roles = "User")] 
        public async Task<IActionResult> MyAppointments()
        {
            string Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await appointmentService.GetMyAppointments(Userid);
            return View(result);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> MyPaper()
        {
            string Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await appointmentService.GetMyPaper(Userid);

            return View(result);
        }



        [HttpGet]
        [Authorize(Roles = "User")] 
        public IActionResult GetAppointmentsCount()
        {
            int acceptState = 2;
            int rejectState = 0;
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appointmentCount = _context.Appointments.Count(a => (a.AppointState == acceptState 
                                                              || a.AppointState == rejectState) 
                                                              && a.ReadMyAppointment == 2
                                                              && a.UserId == userId);
            return Json(appointmentCount);
        }
    }
}
