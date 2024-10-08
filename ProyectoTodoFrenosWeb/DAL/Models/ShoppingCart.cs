﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ShoppingCart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CartId { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime DateAdd { get; set; }
        public ApplicationUser? User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
