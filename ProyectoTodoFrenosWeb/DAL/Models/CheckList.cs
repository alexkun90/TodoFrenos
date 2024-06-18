using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class CheckList
    {
        [Key]
        public int IdCheckList { get; set; }

        public int IdInspect {  get; set; }

        public string CategoryVe { get; set; }

        public string Item {  get; set; }

        public string StateVeh {  get; set; } 


        public virtual ICollection<Inspections> Inspections { get; set; } = new List<Inspections>();
    }
}
