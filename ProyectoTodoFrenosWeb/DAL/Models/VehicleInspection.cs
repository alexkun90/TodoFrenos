using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class VehicleInspection
    {
        [Key]
        public long VehicleInspectionId { get; set; }
        
        [ForeignKey("Vehicle")]
        public long VehicleId { get; set; }
        
        [DisplayName("Nombre del Inspector")]
        public  string? InspectorName { get; set; }
        
        [DisplayName("Fecha de la Inspección")]
        public DateTime InspectionDate { get; set; }

        [DisplayName("Motor")]
        public string? Engine {  get; set; }
        
        [DisplayName("Frenos")]
        public string? Brakes { get; set; }
        
        [DisplayName("Luces")]
        public string? Lights { get; set;}
        
        [DisplayName("Direccion")]
        public string? Steering { get; set; }

        [DisplayName("Neumaticos")]
        public string? Tires { get; set; }

        [DisplayName("Suspensión")]
        public string? Suspension { get; set; }
        
        [DisplayName("Sistema Eléctrico")]
        public string? ElectricalSystem { get; set; }

        [DisplayName("Cambio de Aceite")]
        public int OilChange { get;set;}

        [DisplayName("Fecha de Cambio")]
        public DateTime? DatePerformed { get; set; }

        [DisplayName("Proximo Cambio")]
        public DateTime? NextChangeDue { get; set; }

        [DisplayName("Recomendaciones")]
        public string? Recommendations { get; set; }

        public virtual Vehicle? Vehicle { get; set; }

    }
}
