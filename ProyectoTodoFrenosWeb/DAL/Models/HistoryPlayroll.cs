using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class HistoryPlayroll
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long HistoricoNominaId { get; set; }

        [ForeignKey("Playroll")]
        public long NominaId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public virtual Playroll? Playroll { get; set; }
    }
}
