using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CartItemId { get; set; }
        
        [ForeignKey("ShoppingCart")]
        public long CartId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public virtual Product? Product { get; set; }
        public virtual ShoppingCart? ShoppingCart { get; set; }

    }
}
