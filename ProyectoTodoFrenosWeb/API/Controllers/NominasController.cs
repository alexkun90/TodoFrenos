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
    public class NominasController : ControllerBase
    {
        private readonly TodoFrenosDbContext _context;

        public NominasController(TodoFrenosDbContext context)
        {
            _context = context;
        }

        // GET: api/Nominas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nomina>>> GetNominas()
        {
            return await _context.Nominas.ToListAsync();
        }

        // GET: api/Nominas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Nomina>> GetNomina(int id)
        {
            var nomina = await _context.Nominas.FindAsync(id);

            if (nomina == null)
            {
                return NotFound();
            }

            return nomina;
        }

        // PUT: api/Nominas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNomina(int id, Nomina nomina)
        {
            if (id != nomina.NominaId)
            {
                return BadRequest();
            }

            _context.Entry(nomina).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(nomina);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NominaExists(id))
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

        // POST: api/Nominas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Nomina>> PostNomina(Nomina nomina)
        {
            _context.Nominas.Add(nomina);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNomina", new { id = nomina.NominaId }, nomina);
        }
        // POST: api/Nomina/Calcular
        [HttpPost("CalcularTotal/{nominaId}")]
        public async Task<IActionResult> CalcularTotal(int nominaId)
        {
            // Obtener la nómina y verificar si existe
            var nomina = await _context.Nominas
                                       .Include(n => n.DetallesNominas)
                                       .ThenInclude(d => d.Empleado)
                                       .FirstOrDefaultAsync(n => n.NominaId == nominaId);

            if (nomina == null)
            {
                return NotFound("No se encontró la nómina especificada.");
            }

            // Verificar si la nómina está activa
            if (nomina.Estado != true)
            {
                return BadRequest("La nómina está inactiva y no se puede calcular el total.");
            }

            // Filtrar los detalles de nómina para incluir solo aquellos con el empleado activo
            var detallesNominaActivos = nomina.DetallesNominas
                                  .Where(d => d.Empleado.Estado == true && d.Estado == true)
                                  .ToList();

            if (!detallesNominaActivos.Any())
            {
                return NotFound("No se encontraron detalles de nómina activos para el NominaId especificado.");
            }

            // Sumar los pagos de los detalles de nómina activos
            var total = detallesNominaActivos.Sum(d => d.Pago);

            // Actualizar el total de la nómina
            nomina.TotalNomina = total;
            await _context.SaveChangesAsync();

            return Ok(nomina);
        }


        // DELETE: api/Nominas/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteNominas(int id)
        {
            var nominas = await _context.Nominas.FindAsync(id);
            if (nominas == null)
            {
                return NotFound();
            }
            nominas.Estado = false;
            _context.Nominas.Update(nominas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("Activate/{id}")]
        public async Task<IActionResult> ActivarNominas(int id)
        {
            var nominas = await _context.Nominas.FindAsync(id);
            if (nominas == null)
            {
                return NotFound();
            }
            nominas.Estado = true;
            _context.Nominas.Update(nominas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NominaExists(int id)
        {
            return _context.Nominas.Any(e => e.NominaId == id);
        }
    }
}
