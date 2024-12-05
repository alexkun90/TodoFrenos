using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models;

public partial class Vehicle
{
    [Key]
    public long VehicleId { get; set; }

    public string? UserId { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string TypeVeh { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? Brand { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? ModelYear { get; set; }

    public string? Vin { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? Plate { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime? CreationDate { get; set; }

    public int? CarState { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ApplicationUser? User { get; set; }
}
