using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models;

public partial class Product
{
    [Key]
    [DisplayName("Producto")]
    public long ProductId { get; set; }

    [DisplayName("Categoría")]
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
    [DisplayName("Categoría")]
    public virtual Category? Category { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    //public virtual ICollection<CartItem> CartItems { get; set; }
}
