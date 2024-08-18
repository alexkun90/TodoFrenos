using DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTO
{
    public class PlayRollDTO
    {
        public long NominaId { get; set; }
        public string NombreEmpleado { get; set; }
        public long DeduccionId { get; set; }
        public long SalarioBruto { get; set; }
        public int? HorasTrabajadas { get; set; }
        public int? HorasExtras { get; set; }
        public int? Incapacidades { get; set; }
        public decimal TotalDeduccion { get; set; }
        public decimal? SalarioNeto { get; set; }
        public virtual Deducciones? Deducciones { get; set; }
    }
}
