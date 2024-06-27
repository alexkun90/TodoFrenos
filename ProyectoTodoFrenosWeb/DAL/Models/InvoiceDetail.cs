using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models;

public partial class InvoiceDetail
{
    [Key]
    public long DetailId { get; set; }

    public long MasterId { get; set; }

    public long ProductId { get; set; }

    public int Stock { get; set; }

    public decimal Price { get; set; }

    public decimal SubTotal { get; set; }

    public decimal Taxes { get; set; }

    public decimal Total { get; set; }

    public virtual InvoiceMaster Master { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
