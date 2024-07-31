using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;

using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Drawing;
using Microsoft.AspNetCore.Identity;
using DAL;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly TodoFrenosDbContext _context;
        public AppointmentsController(TodoFrenosDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            return await _context.Appointments.ToListAsync();
        }

        [HttpGet("MyAppointments/{id}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetMyAppointments(string id)
        {
            return await _context.Appointments
                                     .Where(u => u.UserId == id && (u.AppointState == 2 || u.AppointState == 0))
                                     .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(long id)
        {
            var product = await _context.Appointments.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }   

        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppointment", new { id = appointment.AppointId }, appointment);
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(long id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("Accept/{id}")]
        public async Task<IActionResult> AcceptAppointment(long id)
        {
            try
            {
                var appointment = await _context.Appointments.FindAsync(id);
                if (appointment == null)
                {
                    return NotFound(new { message = "Cita no encontrada." });
                }

                appointment.AppointState = 2; 
                _context.Appointments.Update(appointment);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Cita aceptada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al aceptar la cita: {ex.Message}" });
            }
        }

        [HttpPut("Reject/{id}")]
        public async Task<IActionResult> RejectAppointment(long id)
        {
            try
            {
                var appointment = await _context.Appointments.FindAsync(id);
                if (appointment == null)
                {
                    return NotFound(new { message = "Cita no encontrada." });
                }

                appointment.AppointState = 0; 
                _context.Appointments.Update(appointment);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Cita rechazada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al rechazar la cita: {ex.Message}" });
            }
        }

        [HttpPut("Cancel/{id}")]
        public async Task<IActionResult> CancelAppointment(long id)
        {
            try
            {
                var appointment = await _context.Appointments.FindAsync(id);
                if (appointment == null)
                {
                    return NotFound(new { message = "Cita no encontrada." });
                }

                appointment.AppointState = 4;
                _context.Appointments.Update(appointment);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Cita cancelada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al rechazar la cita: {ex.Message}" });
            }
        }
    }
}
