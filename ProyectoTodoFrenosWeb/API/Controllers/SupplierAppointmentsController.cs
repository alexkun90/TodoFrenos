using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierAppointmentsController : ControllerBase
    {
        private readonly TodoFrenosDbContext _context;

        public SupplierAppointmentsController(TodoFrenosDbContext context)
        {
            _context = context;
        }

        // GET: api/SupplierAppointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierAppointment>>> GetSupplierAppointments()
        {
            return await _context.SupplierAppointments.ToListAsync();
        }

        // GET: api/SupplierAppointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierAppointment>> GetSupplierAppointment(long id)
        {
            var supplierAppointment = await _context.SupplierAppointments.FindAsync(id);

            if (supplierAppointment == null)
            {
                return NotFound();
            }

            return supplierAppointment;
        }

        // POST: api/SupplierAppointments
        [HttpPost]
        public async Task<ActionResult<SupplierAppointment>> PostSupplierAppointment(SupplierAppointment supplierAppointment)
        {
            _context.SupplierAppointments.Add(supplierAppointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSupplierAppointment", new { id = supplierAppointment.SupplierAppointId }, supplierAppointment);
        }

        // DELETE: api/SupplierAppointments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplierAppointment(long id)
        {
            var supplierAppointment = await _context.SupplierAppointments.FindAsync(id);
            if (supplierAppointment == null)
            {
                return NotFound();
            }

            _context.SupplierAppointments.Remove(supplierAppointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("Accept/{id}")]
        public async Task<IActionResult> AcceptSupplierAppointment(long id)
        {
            try
            {
                var supplierAppointment = await _context.SupplierAppointments.FindAsync(id);
                if (supplierAppointment == null)
                {
                    return NotFound(new { message = "Cita no encontrada." });
                }

                supplierAppointment.AppointState = 2;
                _context.SupplierAppointments.Update(supplierAppointment);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Cita aceptada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al aceptar la cita: {ex.Message}" });
            }
        }

        [HttpPut("Reject/{id}")]
        public async Task<IActionResult> RejectSupplierAppointment(long id)
        {
            try
            {
                var supplierAppointment = await _context.SupplierAppointments.FindAsync(id);
                if (supplierAppointment == null)
                {
                    return NotFound(new { message = "Cita no encontrada." });
                }

                supplierAppointment.AppointState = 0;
                _context.SupplierAppointments.Update(supplierAppointment);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Cita rechazada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al rechazar la cita: {ex.Message}" });
            }
        }

        private bool SupplierAppointmentExists(long id)
        {
            return _context.SupplierAppointments.Any(e => e.SupplierAppointId == id);
        }
    }
}
