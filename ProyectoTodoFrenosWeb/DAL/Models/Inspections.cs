using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Inspections
    {
        [Key]
        public int IdInsep { get; set; }

        public DateTime DateInspection { get; set; }

        public long IdVehicle { get; set; }

        public int IdMecanico { get; set; }

        public string Recomendation { get; set; }

        public string Observations { get; set; }

        // Navegacion
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        public virtual Vehicle Vehicle { get; set; }
    }
}
