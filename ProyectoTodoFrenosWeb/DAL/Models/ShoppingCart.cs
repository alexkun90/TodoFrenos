using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models;

public partial class ShoppingCart
{
    [Key]
    public long CartId { get; set; }

    public string UserId { get; set; } = null!;

    public long? ProductId { get; set; }

    public DateTime? DateCart { get; set; }

    public int? Stock { get; set; }

    public virtual Product? Product { get; set; }
    public ApplicationUser? User { get; set; }
}
