using DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTO
{
    public class PlayRollDTO
    {
        public long? NominaId { get; set; }
        public string? Cedula {  get; set; }
        public string? NombreCompleto { get; set; }
        public string? Puesto { get; set; }
        public DateTime? FechaInicio {  get; set; }
        public DateTime? FechaFin {  get; set; }
        public decimal? SalarioBase { get; set; }
        public decimal? PlusesSalariales { get; set; }
        public int? HorasExtras { get; set; }
        public int? DiasVacaciones { get; set; }
        public int? Incapacidad { get; set; }
        public string? TipoIncapacidad { get; set; }
        public decimal? DescuentoIncapacitacion { get; set; }
        public decimal? TotalDeduccion { get; set; }
        public decimal? SalarioBruto { get; set; }
        public decimal? SEM { get; set; }
        public decimal? IVM { get; set; }
        public decimal? LPT { get; set; }
        public decimal? ImpuestoRenta { get; set; }
        public decimal? TotalDeducciones { get; set; }
        public decimal? SalarioNeto { get; set; }
    }
}
