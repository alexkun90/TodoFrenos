using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models;

public partial class Nomina
{
    [Key]
    public int NominaId { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFinaliza { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public decimal? TotalNomina { get; set; }
    public bool Estado { get; set; } = true;

    public virtual ICollection<DetallesNomina> DetallesNominas { get; set; } = new List<DetallesNomina>();
}
