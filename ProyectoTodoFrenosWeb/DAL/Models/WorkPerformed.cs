using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class WorkPerformed
    {
        [Key]
        public int IdWorkPer { get; set; }

        public int IdInspe {  get; set; }
        public string Description { get; set; }

        public DateTime NextMaintenance {  get; set; }

        public virtual Inspections Inspe { get; set; }
    }
}
