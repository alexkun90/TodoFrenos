using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Appointment
{
    public long AppointId { get; set; }

    public string UserId { get; set; } = null!;

    public long? VehicleId { get; set; }

    public DateOnly? AppointDate { get; set; }

    public DateTime? AppointCreationDate { get; set; }

    public DateTime? AppointModifyDate { get; set; }

    public string? Reason { get; set; }

    public int? AppointState { get; set; }

    public virtual Vehicle? Vehicle { get; set; }
}
