using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models;

public partial class Empleado
{
    [Key]
    public int Id { get; set; }

    public string? Cedula { get; set; }

    public string? Nombre { get; set; }
    public string? Apellido1 { get; set; }
    public string? Apellido2 { get; set; }

    public string? Puesto { get; set; }

    public DateTime? FechaIngreso { get; set; }
    public bool Estado { get; set; } = true;

    public virtual ICollection<DetallesNomina> DetallesNominas { get; set; } = new List<DetallesNomina>();
}
