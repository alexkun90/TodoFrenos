using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long EmpleadoId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Cedula {  get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? NombreEmpleado { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public  string? ApellidoEmpleado { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public DateTime? FechaContrato { get; set; }

        public int? HorasTrabajadas { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int? CantDiasLaborales { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public decimal? SalarioBase { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public decimal? PlusesSalariales { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? Puesto { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public DateTime? FechaNacimiento { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? Direccion {  get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? EstadoCivil { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? Genero { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string? ContactoEmergencia { get; set; }

        public bool EstadoEmpleado { get; set; } = true;
    }
}
