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

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            return await _context.Appointments.ToListAsync();
        }

        // GET: api/Products/5
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

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(long id, Appointment appointment)
        {
            if (id != appointment.AppointId)
            {
                return BadRequest();
            }

            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(appointment);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        {
          
            {
                DateTime today = DateTime.Now.Date;
                DateTime tomorrow = today.AddDays(1);

                if (appointment.AppointCreationDate == null || appointment.AppointCreationDate <= tomorrow)
                {
                    ModelState.AddModelError(nameof(appointment.AppointCreationDate), "La fecha de cita debe ser a partir de mañana.");
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetAppointment", new { id = appointment.AppointId }, appointment);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(long id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            appointment.AppointState = 0;
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("Activate/{id}")]
        public async Task<IActionResult> ActiveAppointment(long id)
        {
            var appoint = await _context.Appointments.FindAsync(id);
            if (appoint == null)
            {
                return NotFound();
            }

            appoint.AppointState = 1;
            _context.Appointments.Update(appoint);
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

                appointment.AppointState = 1; 
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

                appointment.AppointState = -1; 
                _context.Appointments.Update(appointment);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Cita rechazada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al rechazar la cita: {ex.Message}" });
            }
        }

        private bool AppointmentExists(long id)
        {
            return _context.Appointments.Any(e => e.AppointId == id);
        }
    }
}
