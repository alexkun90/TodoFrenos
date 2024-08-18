using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class PlanillaEmpleado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PlanillaEmpleadoId { get; set; }
        [ForeignKey("Playroll")]
        public long NominaId { get; set; }
        public DateTime FechaPago {  get; set; }
        public decimal SalarioNetoFinal { get; set; }
        public virtual Playroll? Playroll { get; set;}
    }
}
