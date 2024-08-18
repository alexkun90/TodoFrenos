using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class HistoricoPlanilla
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long HistoricoPlanillaId { get; set; }
        public long PlanillaEmpleadoId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public virtual PlanillaEmpleado? PlanillaEmpleado { get; set; }
    }
}
