using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Product
{
    public long ProductId { get; set; }

    public long? CategoryId { get; set; }

    public string? ProductName { get; set; }

    public int? Stock { get; set; }

    public decimal? Price { get; set; }

    public byte[]? ImageProduct { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
}
