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
    public class VehicleInspectionsController : ControllerBase
    {
        private readonly TodoFrenosDbContext _context;

        public VehicleInspectionsController(TodoFrenosDbContext context)
        {
            _context = context;
        }

        // GET: api/VehicleInspections
        [HttpGet("List/{id}")]
        public async Task<ActionResult<IEnumerable<VehicleInspection>>> GetVehicleInspections(long id)
        {
            return await _context.VehicleInspections
                                     .Where(v => v.VehicleId == id)
                                     .ToListAsync();
        }

        // GET: api/VehicleInspections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleInspection>> GetVehicleInspection(long id)
        {
            var vehicleInspection = await _context.VehicleInspections.FindAsync(id);

            if (vehicleInspection == null)
            {
                return NotFound();
            }

            return vehicleInspection;
        }

        // PUT: api/VehicleInspections/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleInspection(long id, VehicleInspection vehicleInspection)
        {
            if (id != vehicleInspection.VehicleInspectionId)
            {
                return BadRequest();
            }

            _context.Entry(vehicleInspection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(vehicleInspection);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleInspectionExists(id))
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

        // POST: api/VehicleInspections
        [HttpPost]
        public async Task<ActionResult<VehicleInspection>> PostVehicleInspection(VehicleInspection vehicleInspection)
        {
            _context.VehicleInspections.Add(vehicleInspection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicleInspection", new { id = vehicleInspection.VehicleInspectionId }, vehicleInspection);
        }

        // DELETE: api/VehicleInspections/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleInspection(long id)
        {
            var vehicleInspection = await _context.VehicleInspections.FindAsync(id);
            if (vehicleInspection == null)
            {
                return NotFound();
            }

            _context.VehicleInspections.Remove(vehicleInspection);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleInspectionExists(long id)
        {
            return _context.VehicleInspections.Any(e => e.VehicleInspectionId == id);
        }
    }
}
