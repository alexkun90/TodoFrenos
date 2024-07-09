using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models;

public partial class Appointment
{
    [Key]
    public long AppointId { get; set; }

    public string UserId { get; set; } = null!;

    public long? VehicleId { get; set; }

    public DateOnly? AppointDate { get; set; }

    [Required(ErrorMessage = "El campo Fecha de Cita es obligatorio.")]
    [DataType(DataType.Date)]
    
    public DateTime? AppointCreationDate { get; set; }

    public DateTime? AppointModifyDate { get; set; }

    [Required(ErrorMessage = "El campo Motivo es obligatorio.")]
    public string? Reason { get; set; }

    [Required(ErrorMessage = "El campo Estado obligatorio.")]
    public int? AppointState { get; set; } = 0;

    public virtual Vehicle? Vehicle { get; set; }

    public ApplicationUser? User { get; set; }

    
    }


