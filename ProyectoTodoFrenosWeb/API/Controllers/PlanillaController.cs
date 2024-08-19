using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanillaController : ControllerBase
    {
        private readonly TodoFrenosDbContext _context;

        public PlanillaController(TodoFrenosDbContext context)
        {
            _context = context;
        }

        [HttpGet("List/{nominaId}")]
        public async Task<ActionResult<IEnumerable<PlanillaEmpleado>>> GetAllPlanilla(long nominaId)
        {
            var planilla = await _context.PlanillaEmpleados
                           .OrderBy(p => p.FechaPago)
                           .Where(p => p.NominaId == nominaId)
                           .ToListAsync();   
            return Ok(planilla);
        }

        [HttpGet("Details/{planillaId}")]
        public async Task<ActionResult<PlanillaEmpleado>> GetPlayrollDetails(long planillaId)
        {
            var planillaDetails = await _context.PlanillaEmpleados.FindAsync(planillaId);
            if(planillaDetails == null)
            {
                return NotFound();
            }

            return Ok(planillaDetails);
        }

        [HttpPost("{nominaId}")]
        public async Task<IActionResult> CreatePlanillaEmpleado(long nominaId, PlanillaEmpleado planillaEmpleado)
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
            planillaEmpleado.Cedula = playroll.Deducciones.PlayrollDetail.Employee.Cedula;
            planillaEmpleado.NombreEmpleado = playroll.Deducciones.PlayrollDetail.Employee.NombreEmpleado;
            planillaEmpleado.ApellidoEmpleado = playroll.Deducciones.PlayrollDetail.Employee.ApellidoEmpleado;
            planillaEmpleado.Puesto = playroll.Deducciones.PlayrollDetail.Employee.Puesto;
            
            planillaEmpleado.SalarioBruto = playroll.Deducciones.PlayrollDetail.SalarioBruto / 2;

            planillaEmpleado.SEM = playroll.Deducciones.SEM / 2;
            planillaEmpleado.IVM = playroll.Deducciones.IVM / 2;
            planillaEmpleado.LPT = playroll.Deducciones.LPT / 2;
            planillaEmpleado.ImpuestoRenta = playroll.Deducciones.ImpuestoRenta / 2;

            planillaEmpleado.TotalDeducciones = playroll.Deducciones.TotalDeduccion / 2;
            planillaEmpleado.SalarioNetoFinal = playroll.SalarioNeto / 2;
            _context.PlanillaEmpleados.Add(planillaEmpleado);
            await _context.SaveChangesAsync();

            return Ok(planillaEmpleado);
        }
    }
}
