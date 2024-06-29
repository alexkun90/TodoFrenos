using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models;

public partial class InvoiceMaster
{
    [Key]
    public long MasterId { get; set; }

    public string UserId { get; set; } = null!;

    public DateTime DatePurchase { get; set; }

    public decimal SubTotal { get; set; }

    public decimal Taxes { get; set; }

    public decimal Total { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();

    public ApplicationUser? User { get; set; }
}
