using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class DisabilityEmployee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IncapacidadId { get; set; }
        [ForeignKey("Employee")]
        public long EmpleadoId { get; set; }
        public int Entidad { get; set; }//Ins-Caja
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin {  get; set; }
        public int DiasIncapacitado { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}
