using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly TodoFrenosDbContext _context;

        public SuppliersController(TodoFrenosDbContext context)
        {
            _context = context;
        }

        // GET: api/Suppliers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Suppliers>>> GetSuppliers()
        {
            return await _context.Suppliers.Where(s => s.SupplierState != -1).ToListAsync();
        }

        // GET: api/Suppliers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Suppliers>> GetSupplier(long id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null || supplier.SupplierState == -1)
            {
                return NotFound();
            }

            return supplier;
        }

        // PUT: api/Suppliers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupplier(long id, Suppliers supplier)
        {
            if (id != supplier.SupplierId)
            {
                return BadRequest();
            }

            _context.Entry(supplier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(supplier);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Suppliers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Suppliers>> PostSupplier(Suppliers supplier)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSupplier", new { id = supplier.SupplierId }, supplier);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(long id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            supplier.SupplierState = -1; // Borrado lógico
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("Activate/{id}")]
        public async Task<IActionResult> ActivateSupplier(long id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            supplier.SupplierState = 1; // Reactivar
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("Accept/{id}")]
        public async Task<IActionResult> AcceptSupplier(long id)
        {
            try
            {
                var supplier = await _context.Suppliers.FindAsync(id);
                if (supplier == null)
                {
                    return NotFound(new { message = "Cita no encontrada." });
                }

                supplier.SupplierState = 1;
                _context.Suppliers.Update(supplier);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Cita aceptada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al aceptar la cita: {ex.Message}" });
            }
        }

        [HttpPut("Reject/{id}")]
        public async Task<IActionResult> RejectSupplier(long id)
        {
            try
            {
                var supplier = await _context.Suppliers.FindAsync(id);
                if (supplier == null)
                {
                    return NotFound(new { message = "Cita no encontrada." });
                }

                supplier.SupplierState = -1;
                _context.Suppliers.Update(supplier);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Cita rechazada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al rechazar la cita: {ex.Message}" });
            }
        }



        private bool SupplierExists(long id)
        {
            return _context.Suppliers.Any(e => e.SupplierId == id);
        }
    }
}
