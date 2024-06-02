using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Vehicle
{
    public long VehicleId { get; set; }

    public string UserId { get; set; } = null!;

    public string? Brand { get; set; }

    public string? RegistrationCar { get; set; }

    public DateTime? CreationDate { get; set; }

    public int? CarState { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
