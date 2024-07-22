using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class SupplierAppointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SupplierAppointId { get; set; }

        public long SupplierListId { get; set; }

        public string? SupplierEmail { get; set; }

        [Required(ErrorMessage = "El campo Fecha de Cita es obligatorio.")]
        [DataType(DataType.Date)]
        public DateTime? AppointCreationDate { get; set; }
        
        [Required(ErrorMessage = "El campo Motivo es obligatorio.")]
        public string? Reason { get; set; }

        [Required(ErrorMessage = "El campo Estado obligatorio.")]
        public int? AppointState { get; set; } = 1;

        public virtual SupplierList? SupplierList { get; set; }
    }
}
