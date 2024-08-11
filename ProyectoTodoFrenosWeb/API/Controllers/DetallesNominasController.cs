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
    public class DetallesNominasController : ControllerBase
    {
        private readonly TodoFrenosDbContext _context;

        public DetallesNominasController(TodoFrenosDbContext context)
        {
            _context = context;
        }

        // GET: api/DetallesNominas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetallesNomina>>> GetDetallesNominas()
        {

            return await _context.DetallesNominas.ToListAsync();
        }

        // GET: api/DetallesNominas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetallesNomina>> GetDetallesNomina(int id)
        {
            var detallesNomina = await _context.DetallesNominas.FindAsync(id);

            if (detallesNomina == null)
            {
                return NotFound();
            }

            return detallesNomina;
        }

        // PUT: api/DetallesNominas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetallesNomina(int id, DetallesNomina detallesNomina)
        {
            if (id != detallesNomina.DetalleId)
            {
                return BadRequest();
            }

            // Realizar cálculos antes de guardar
            RealizarCalculos(detallesNomina);

            _context.Entry(detallesNomina).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(detallesNomina);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetallesNominaExists(id))
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

        // POST: api/DetallesNominas
        [HttpPost]
        public async Task<ActionResult<DetallesNomina>> PostDetallesNomina(DetallesNomina detallesNomina)
        {
            // Realizar cálculos antes de guardar
            RealizarCalculos(detallesNomina);

            _context.DetallesNominas.Add(detallesNomina);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetallesNomina", new { id = detallesNomina.DetalleId }, detallesNomina);
        }


        // DELETE: api/DetallesNominas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetallesNominas(int id)
        {
            var detallesNomina = await _context.DetallesNominas.FindAsync(id);
            if (detallesNomina == null)
            {
                return NotFound();
            }
            detallesNomina.Estado = false;
            _context.DetallesNominas.Update(detallesNomina);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("Activate/{id}")]
        public async Task<IActionResult> ActivarDetallesNominas(int id)
        {
            var detallesNomina = await _context.DetallesNominas.FindAsync(id);
            if (detallesNomina == null)
            {
                return NotFound();
            }
            detallesNomina.Estado = true;
            _context.DetallesNominas.Update(detallesNomina);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetallesNominaExists(int id)
        {
            return _context.DetallesNominas.Any(e => e.DetalleId == id);
        }
        private void RealizarCalculos(DetallesNomina detallesNomina)
        {
            // Verificar si el empleado está activo
            if (detallesNomina.Empleado.Estado != true || detallesNomina.Nomina.Estado != true)
            {
                return; // Salir del método si el estado no es True
            }

            if (detallesNomina.CantidadHoras.HasValue && detallesNomina.Hora.HasValue)
            {
                detallesNomina.Diario = detallesNomina.CantidadHoras.Value * detallesNomina.Hora.Value;
                detallesNomina.Semanal = detallesNomina.Diario * 7;
                detallesNomina.Mensual = detallesNomina.Semanal * 4;
                detallesNomina.Pago = detallesNomina.Semanal;
            }

            if (detallesNomina.CantidadHorasExtra.HasValue && detallesNomina.HorasExtra.HasValue)
            {
                detallesNomina.Pago += detallesNomina.CantidadHorasExtra.Value * detallesNomina.HorasExtra.Value;
            }

            if (detallesNomina.Ccss.HasValue)
            {
                detallesNomina.Pago -= detallesNomina.Ccss.Value;
            }

            if (detallesNomina.Vales.HasValue)
            {
                detallesNomina.Pago -= detallesNomina.Vales.Value;
            }
        }
    }
}
