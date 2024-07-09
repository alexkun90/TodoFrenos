using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Suppliers
    {
        [Key]
        public long SupplierId { get; set; }

        [Required(ErrorMessage = "El campo Email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo Email no es una dirección de correo electrónico válida.")]
        public string Email { get; set; }
        

        public DateOnly? SuppliertDate { get; set; }

        [Required(ErrorMessage = "El campo Fecha de Cita es obligatorio.")]
        [DataType(DataType.Date)]

        public DateTime? SupplierCreationDate { get; set; }

        public DateTime? SupplierModifyDate { get; set; }

        [Required(ErrorMessage = "El campo Motivo es obligatorio.")]
        public string? Cause { get; set; }

        [Required(ErrorMessage = "El campo Estado obligatorio.")]
        public int SupplierState { get; set; }


    }
}
