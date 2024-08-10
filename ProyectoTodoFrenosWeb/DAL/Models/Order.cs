using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderId { get; set; }
        public string UserId { get; set; }
        public long CodigoOrder { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RetirementDate {  get; set; }
        public decimal SubTotal { get; set; } 
        public decimal Tax { get; set; } 
        public decimal Total { get; set; }
        public ApplicationUser? User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
