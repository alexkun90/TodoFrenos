using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Deducciones
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DeduccionId { get; set; }
        [ForeignKey("Employee")]
        public long EmployeeId { get; set; }
        public decimal? SalarioBruto { get; set; }
        public decimal? SEM { get; set; }
        public decimal? IVM { get; set; }
        public decimal? LPT { get; set; }
        public decimal? ImpuestoRenta { get; set; }
        public decimal? TotalDeduccion { get; set; }
        public virtual Employee? Employee { get; set; }

    }
}
