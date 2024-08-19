using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using API.DTO;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayRollDTO>>> GetAllPlayrolls()
        {
            var playrolls = await _context.Playrolls
                .Include(p => p.Deducciones)
                .ThenInclude(d => d.PlayrollDetail)
                .ThenInclude(pd => pd.Employee)
                .OrderBy(p => p.FechaInicio)
                .ToListAsync();

            var playRollDTOs = playrolls.Select(playroll => new PlayRollDTO
            {
                NominaId = playroll.NominaId,
                Cedula = playroll.Deducciones.PlayrollDetail.Employee.Cedula,
                NombreCompleto = $"{playroll.Deducciones.PlayrollDetail.Employee.NombreEmpleado} {playroll.Deducciones.PlayrollDetail.Employee.ApellidoEmpleado}",
                Puesto = playroll.Deducciones.PlayrollDetail.Employee.Puesto,
                FechaInicio = playroll.FechaInicio,
                FechaFin = playroll.FechaFin,
                SalarioNeto = playroll.SalarioNeto
            }).ToList();

            return Ok(playRollDTOs);
        }

        // GET: api/Playrolls
        [HttpGet("{nominaId}")]
        public async Task<ActionResult<PlayRollDTO>> GetPlayrollDetails(long nominaId)
        {
            var playroll = await _context.Playrolls
                .Where(p => p.NominaId == nominaId)
                .Include(p => p.Deducciones)
                .ThenInclude(d => d.PlayrollDetail)
                .ThenInclude(pd => pd.Employee)
                .FirstOrDefaultAsync();

            if (playroll == null)
            {
                return NotFound();
            }

            var playRollDTO = new PlayRollDTO
            {
                NominaId = playroll.NominaId,
                Cedula = playroll.Deducciones.PlayrollDetail.Employee.Cedula,
                NombreCompleto = $"{playroll.Deducciones.PlayrollDetail.Employee.NombreEmpleado} {playroll.Deducciones.PlayrollDetail.Employee.ApellidoEmpleado}",
                Puesto = playroll.Deducciones.PlayrollDetail.Employee.Puesto,
                FechaInicio = playroll.FechaInicio,
                FechaFin = playroll.FechaFin,
                SalarioBase = playroll.Deducciones.PlayrollDetail.Employee.SalarioBase,
                PlusesSalariales = playroll.Deducciones.PlayrollDetail.Employee.PlusesSalariales,
                HorasExtras = playroll.Deducciones.PlayrollDetail.HorasExtras,
                DiasVacaciones = playroll.Deducciones.PlayrollDetail.DiasVacaciones,
                Incapacidad = playroll.Deducciones.PlayrollDetail.Incapacidad,               
                SalarioBruto = playroll.Deducciones.SalarioBruto,
                SEM = playroll.Deducciones.SEM,
                IVM = playroll.Deducciones.IVM,
                LPT = playroll.Deducciones.LPT,
                ImpuestoRenta = playroll.Deducciones.ImpuestoRenta,
                TotalDeduccion = playroll.Deducciones.TotalDeduccion,
                SalarioNeto = playroll.SalarioNeto
            };

            return Ok(playRollDTO);
        }

        //Post Nomina
        [HttpPost("{employeeId}")]
        public async Task<IActionResult> CreateFullNomina(long employeeId, PlayrollDetail playrollDetail)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null)
            {
                return NotFound();
            }
            var salarioDiario = employee.SalarioBase / employee.CantDiasLaborales;
            
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
            else if(playrollDetail.TipoIncapacidad == null && playrollDetail.Incapacidad == 0)
            {
                montoIncapacidad = 0;
            }
            
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

            return Ok(playrollDetail);
        }

        

        
    }
}
