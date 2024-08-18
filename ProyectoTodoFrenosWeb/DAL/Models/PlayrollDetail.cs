using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class PlayrollDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NominaDetalleId { get; set; }      
        
        [ForeignKey("Employee")]
        public long EmployeeId { get; set; }
        public int HorasExtras { get; set; }
        public int DiasVacaciones { get; set; }
        public decimal Incapacidad { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal? SalarioBruto { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}
