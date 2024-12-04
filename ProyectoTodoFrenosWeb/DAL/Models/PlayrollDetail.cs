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

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public int? HorasExtras { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public int? DiasVacaciones { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public int? Incapacidad { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string? TipoIncapacidad { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal? SalarioBruto { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}
