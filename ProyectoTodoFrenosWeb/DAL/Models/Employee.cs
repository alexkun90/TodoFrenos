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
        public string Cedula {  get; set; }
        public string? NombreEmpleado { get; set; }
        public  string? ApellidoEmpleado { get; set; }       
        public DateTime? FechaContrato { get; set; }
        public int? HorasTrabajadas { get; set; }
        public decimal? SalarioBase { get; set; }
        public decimal? PlusesSalariales { get; set; }
        public string? Puesto { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Direccion {  get; set; }
        public string? EstadoCivil { get; set; }
        public string? Genero { get; set; }
        public string? ContactoEmergencia { get; set; }
        public bool EstadoEmpleado { get; set; } = true;
    }
}
