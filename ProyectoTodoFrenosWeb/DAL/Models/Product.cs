using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models;

public partial class Product
{
    [Key]
    public long ProductId { get; set; }

    [DisplayName("Categoria")]
    public long? CategoryId { get; set; }

    public string? ProductName { get; set; }

    public int? Stock { get; set; }

    public decimal? Price { get; set; }

    public bool StateProdc { get; set; } = true;
    public byte[]? ImageProduct { get; set; }
    [DisplayName("Categoria")]
    public virtual Category? Category { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
}
