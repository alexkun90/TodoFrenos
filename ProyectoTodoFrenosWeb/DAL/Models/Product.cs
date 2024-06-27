using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DAL.Models;

public partial class Product
{
    [DisplayName("Producto")]
    public long ProductId { get; set; }

    [DisplayName("Categoria")]
    public long? CategoryId { get; set; }
    [DisplayName("Nombre")]
    public string? ProductName { get; set; }

    public int? Stock { get; set; }
    [DisplayName("Precio")]
    public decimal? Price { get; set; }
    [DisplayName("Estado")]
    public bool StateProdc { get; set; } = true;
    [DisplayName("Imagen")]
    public byte[]? ImageProduct { get; set; }
    [DisplayName("Categoria")]
    public virtual Category? Category { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
}
