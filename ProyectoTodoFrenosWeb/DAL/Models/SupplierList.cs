using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class SupplierList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SupplierListId { get; set; }

        [DisplayName("NombreProveedor")]
        public string? SupplierName { get; set; }

        [DisplayName("EmailProveedor")]
        public string? SupplierEmail { get; set; }
    }
}
