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


        //Post Nomina
        [HttpPost("{employeeId}")]
        public async Task<IActionResult> CreateFullNomina(long employeeId, PlayrollDetail playrollDetail)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null)
            {
                return NotFound();
            }
            var salarioDiario = employee.SalarioBase + employee.CantDiasLaborales;
            
            decimal? montoIncapacidad = 0;
            if(playrollDetail.TipoIncapacidad == "CCSS" && playrollDetail.Incapacidad <= 3)
            {
                montoIncapacidad = playrollDetail.Incapacidad * (salarioDiario * 0.50m);
            }
            else if (playrollDetail.TipoIncapacidad == "CCSS" && playrollDetail.Incapacidad  > 3)
            {
                montoIncapacidad = ((playrollDetail.Incapacidad-3) * salarioDiario * 0.60m) + (3 * (salarioDiario * 0.50m));
            }
            else if (playrollDetail.TipoIncapacidad == "INS" && playrollDetail.Incapacidad <= 45)
            {
                montoIncapacidad = playrollDetail.Incapacidad * (salarioDiario * 0.60m);
            }
            else if (playrollDetail.TipoIncapacidad == "INS" && playrollDetail.Incapacidad > 45)
            {
                montoIncapacidad = ((playrollDetail.Incapacidad - 45) * salarioDiario * 0.67m) + (45 * (salarioDiario * 0.60m));
            }

            playrollDetail.EmployeeId = employeeId;
            
            playrollDetail.SalarioBruto = 
                employee.SalarioBase + employee.PlusesSalariales 
                + (playrollDetail.HorasExtras * 1.5m * 8) 
                - (playrollDetail.DiasVacaciones * salarioDiario) 
                - montoIncapacidad;

            _context.PlayrollDetails.Add(playrollDetail);
            await _context.SaveChangesAsync(); 
            
            var monto = playrollDetail.SalarioBruto;
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

            var deduccion = new Deducciones
            {
                NominaDetalleId = playrollDetail.NominaDetalleId,
                SalarioBruto = monto,
                SEM = monto * 0.0550m,
                IVM = monto * 0.0417m,
                LPT = monto * 0.01m,
                ImpuestoRenta = impuestoR,
                TotalDeduccion = (monto * 0.0550m) + (monto * 0.0417m) + (monto * 0.01m) + (impuestoR)
            };
            _context.Deducciones.Add(deduccion);
            await _context.SaveChangesAsync();

            var playroll = new Playroll
            {
                DeduccionId = deduccion.DeduccionId,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddMonths(1),
                SalarioNeto = monto - deduccion.TotalDeduccion
            };

            _context.Playrolls.Add(playroll);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayrollDetail", new { id = playrollDetail.NominaDetalleId }, playrollDetail);
        }

        //POST: api/Deducciones
        //[HttpPost]
        //public async Task<ActionResult<Deducciones>> PostDeducciones(long employeeId, Deducciones deducciones)
        //{
        //    var employee = await _context.Employees.FindAsync(employeeId);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    deducciones.EmployeeId = employeeId;

        //    var monto = employee.PlusesSalariales + employee.SalarioBase;
        //    decimal? impuestoR = 0m;

        //    if (monto <= 929000m)
        //    {
        //        impuestoR = 0; /* 0% */
        //    }
        //    else if (monto > 929000m && monto <= 1363000m)
        //    {
        //        impuestoR = (monto - 929000m) * 0.10m; /* 10% */
        //    }
        //    else if (monto > 1363000m && monto <= 2392000m)
        //    {
        //        impuestoR = (1363000m - 929000m) * 0.10m + (monto - 1363000m) * 0.15m; /* 10% y 15% */
        //    }
        //    else if (monto > 2392000m && monto <= 4783000m)
        //    {
        //        impuestoR = (1363000m - 929000m) * 0.10m + (2392000m - 1363000m) * 0.15m + (monto - 2392000m) * 0.20m; /* 10%, 15% y 20% */
        //    }
        //    else if (monto > 4783000m)
        //    {
        //        impuestoR = (1363000m - 929000m) * 0.10m + (2392000m - 1363000m) * 0.15m + (4783000m - 2392000m) * 0.20m + (monto - 4783000m) * 0.25m; /* 10%, 15%, 20% y 25% */
        //    }

        //    deducciones.SalarioBruto = monto;
        //    deducciones.SEM = monto * 0.0550m; /* 5.50% */
        //    deducciones.IVM = monto * 0.0417m; /* 4.17% */
        //    deducciones.LPT = monto * 0.01m; /* 1.00% */
        //    deducciones.ImpuestoRenta = impuestoR;
        //    deducciones.TotalDeduccion = deducciones.SEM + deducciones.IVM + deducciones.LPT + deducciones.ImpuestoRenta;

        //    _context.Deducciones.Add(deducciones);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetDeduccion", new { id = deducciones.DeduccionId }, deducciones);
        //}

        
    }
}
