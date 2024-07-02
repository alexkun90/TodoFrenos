using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models;

public partial class Vehicle
{
    [Key]
    public long VehicleId { get; set; }

    public string? UserId { get; set; }

    public string TypeVeh { get; set; }

    public string? Brand { get; set; }

    public string? ModelYear { get; set; }

    public string? Vin { get; set; }

    public string? Plate { get; set; }

    public DateTime? CreationDate { get; set; }

    public int? CarState { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ApplicationUser? User { get; set; }
}
