using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Asistence
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AsistenciaId { get; set; }
        [ForeignKey("Employee")]
        public long EmpleadoId { get; set; }
        public DateTime Fecha {  get; set; }
        public string HoraLlegada { get; set; }
        public string HoraSalida {  get; set; }
        public string TipoAsistencia {  get; set; } //Presente,Ausente,Incapacitado,Licencia Maternidad,Vacaciones
        public virtual Employee? Employee { get; set; }
    }
}
