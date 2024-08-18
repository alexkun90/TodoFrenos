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
    public class PlayrollsController : ControllerBase
    {
        private readonly TodoFrenosDbContext _context;

        public PlayrollsController(TodoFrenosDbContext context)
        {
            _context = context;
        }

        // GET: api/Playrolls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Playroll>>> GetPlayrolls()
        {
            return await _context.Playrolls.ToListAsync();
        }

        // GET: api/Playrolls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Playroll>> GetPlayroll(long id)
        {
            var playroll = await _context.Playrolls.FindAsync(id);

            if (playroll == null)
            {
                return NotFound();
            }

            return playroll;
        }

        // POST: api/Deducciones
        [HttpPost]
        public async Task<ActionResult<Deducciones>> PostDeducciones(long employeeId, Deducciones deducciones)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null)
            {
                return NotFound();
            }

            deducciones.EmployeeId = employeeId;

            var monto = employee.PlusesSalariales + employee.SalarioBase;
            decimal? impuestoR = 0m;

            if (monto <= 929000m)
            {
                impuestoR = 0; /* 0% */
            }
            else if (monto > 929000m && monto <= 1363000m)
            {
                impuestoR = (monto - 929000m) * 0.10m; /* 10% */
            }
            else if (monto > 1363000m && monto <= 2392000m)
            {
                impuestoR = (1363000m - 929000m) * 0.10m + (monto - 1363000m) * 0.15m; /* 10% y 15% */
            }
            else if (monto > 2392000m && monto <= 4783000m)
            {
                impuestoR = (1363000m - 929000m) * 0.10m + (2392000m - 1363000m) * 0.15m + (monto - 2392000m) * 0.20m; /* 10%, 15% y 20% */
            }
            else if (monto > 4783000m)
            {
                impuestoR = (1363000m - 929000m) * 0.10m + (2392000m - 1363000m) * 0.15m + (4783000m - 2392000m) * 0.20m + (monto - 4783000m) * 0.25m; /* 10%, 15%, 20% y 25% */
            }

            deducciones.SalarioBruto = monto;
            deducciones.SEM = monto * 0.0550m; /* 5.50% */
            deducciones.IVM = monto * 0.0417m; /* 4.17% */
            deducciones.LPT = monto * 0.01m; /* 1.00% */
            deducciones.ImpuestoRenta = impuestoR;
            deducciones.TotalDeduccion = deducciones.SEM + deducciones.IVM + deducciones.LPT + deducciones.ImpuestoRenta;

            _context.Deducciones.Add(deducciones);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeduccion", new { id = deducciones.DeduccionId }, deducciones);
        }

        // POST: api/Playrolls
        [HttpPost]
        public async Task<ActionResult<Playroll>> PostPlayroll(Playroll playroll)
        {

            var deduccion = await _context.Deducciones
           .Include(d => d.Employee) 
           .FirstOrDefaultAsync(d => d.DeduccionId == playroll.DeduccionId);

            if (deduccion == null)
            {
                return NotFound(); 
            }

            playroll.SalarioNeto = deduccion.SalarioBruto - deduccion.TotalDeduccion;

            _context.Playrolls.Add(playroll);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayroll", new { id = playroll.NominaId }, playroll);
        }

        
    }
}
