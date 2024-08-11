using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models;

public partial class DetallesNomina
{
    [Key]
    public int DetalleId { get; set; }

    public int? NominaId { get; set; }

    public int? EmpleadoId { get; set; }

    public decimal? SalarioBase { get; set; }

    public decimal? Semanal { get; set; }

    public decimal? Mensual { get; set; }

    public decimal? Diario { get; set; }

    public decimal? Hora { get; set; }

    public int? CantidadHoras { get; set; }

    public decimal? HorasExtra { get; set; }

    public int? CantidadHorasExtra { get; set; }

    public decimal? Ccss { get; set; }

    public decimal? Vales { get; set; }

    public decimal? Pago { get; set; }

    public bool Estado { get; set; } = true;
    
    public virtual Empleado? Empleado { get; set; }

    public virtual Nomina? Nomina { get; set; }
}
