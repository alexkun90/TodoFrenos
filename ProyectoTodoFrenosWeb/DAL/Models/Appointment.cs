using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models;

public partial class Appointment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long AppointId { get; set; }

    public string UserId { get; set; } = null!;

    [Required(ErrorMessage = "La fecha de creación de la cita es obligatoria.")]
    [DataType(DataType.Date)]
    
    public DateTime? AppointCreationDate { get; set; }

    [Required(ErrorMessage = "El motivo es obligatorio.")]
    public string? Reason { get; set; }

    public int? AppointState { get; set; } = 1;

    public bool? StateAppointment {  get; set; }

    public int? ReadMyAppointment { get; set; } = 2;

    public ApplicationUser? User { get; set; }
    
    }


