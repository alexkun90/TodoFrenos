using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderDetailId { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public decimal? PriceWithTax { get; set; }
        public int? Quantity { get; set; }
        public decimal? Subtotal { get; set; } 
        public decimal ? Total { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Order? Order { get; set; }
    }
}
