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
    public class AsistencesController : ControllerBase
    {
        private readonly TodoFrenosDbContext _context;

        public AsistencesController(TodoFrenosDbContext context)
        {
            _context = context;
        }

        // GET: api/Asistences
        [HttpGet("List/{id}")]
        public async Task<ActionResult<IEnumerable<Asistence>>> GetAsistences(long id)
        {
            return await _context.Asistences
                .Where(a => a.EmpleadoId == id)
                .ToListAsync();
        }

        // GET: api/Asistences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asistence>> GetAsistence(long id)
        {
            var asistence = await _context.Asistences.FindAsync(id);

            if (asistence == null)
            {
                return NotFound();
            }

            return asistence;
        }
        
        // POST: api/Asistences
        [HttpPost]
        public async Task<ActionResult<Asistence>> PostAsistence(Asistence asistence)
        {
            _context.Asistences.Add(asistence);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsistence", new { id = asistence.AsistenciaId }, asistence);
        }

        /*
                // PUT: api/Asistences/5
                // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
                [HttpPut("{id}")]
                public async Task<IActionResult> PutAsistence(long id, Asistence asistence)
                {
                    if (id != asistence.AsistenciaId)
                    {
                        return BadRequest();
                    }

                    _context.Entry(asistence).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AsistenceExists(id))
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
        */

        // DELETE: api/Asistences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsistence(long id)
        {
            var asistence = await _context.Asistences.FindAsync(id);
            if (asistence == null)
            {
                return NotFound();
            }

            _context.Asistences.Remove(asistence);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AsistenceExists(long id)
        {
            return _context.Asistences.Any(e => e.AsistenciaId == id);
        }
    }
}
