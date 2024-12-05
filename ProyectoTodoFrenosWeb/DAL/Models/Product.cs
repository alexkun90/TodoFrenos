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

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? ProductName { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public int? Stock { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [DisplayName("Precio")]
    public decimal? Price { get; set; }

    [DisplayName("Estado")]
    public bool StateProdc { get; set; } = true;

    [DisplayName("Imagen")]
    public byte[]? ImageProduct { get; set; }

    [DisplayName("Categoría")]
    public virtual Category? Category { get; set; }
}
