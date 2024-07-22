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

    [Required(ErrorMessage = "El campo Fecha de Cita es obligatorio.")]
    [DataType(DataType.Date)]
    
    public DateTime? AppointCreationDate { get; set; }

    [Required(ErrorMessage = "El campo Motivo es obligatorio.")]
    public string? Reason { get; set; }

    [Required(ErrorMessage = "El campo Estado obligatorio.")]
    public int? AppointState { get; set; } = 1;

    public ApplicationUser? User { get; set; }
    
    }


