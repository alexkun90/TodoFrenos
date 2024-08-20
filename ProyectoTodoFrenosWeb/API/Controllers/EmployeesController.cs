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
    public class EmployeesController : ControllerBase
    {
        private readonly TodoFrenosDbContext _context;

        public EmployeesController(TodoFrenosDbContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(long id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            var cedulaExist = await _context.Employees
                             .FirstOrDefaultAsync(e => e.Cedula == employee.Cedula);
            if(cedulaExist != null)
            {
                return Conflict("Está cedula de empleado ya está anteriormente registrada");
            }
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.EmpleadoId }, employee);
        }

        
        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(long id, Employee employee)
        {
            if (id != employee.EmpleadoId)
            {
                return BadRequest();
            }
           
            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(employee);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(long id)
        {
            var empleados = await _context.Employees.FindAsync(id);
            if (empleados == null)
            {
                return NotFound();
            }
            empleados.EstadoEmpleado = false;
            _context.Employees.Update(empleados);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("Activate/{id}")]
        public async Task<IActionResult> ActivateEmployee(long id)
        {
            var empleados = await _context.Employees.FindAsync(id);
            if (empleados == null)
            {
                return NotFound();
            }
            empleados.EstadoEmpleado = true;
            _context.Employees.Update(empleados);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(long id)
        {
            return _context.Employees.Any(e => e.EmpleadoId == id);
        }
    }
}
