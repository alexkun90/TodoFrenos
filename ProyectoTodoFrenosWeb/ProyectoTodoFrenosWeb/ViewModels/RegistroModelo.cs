using System.ComponentModel.DataAnnotations;

namespace ProyectoTodoFrenosWeb.ViewModels
{
    public class RegistroModelo
    {
        public RegistroModelo()
        {
            
            Roles = new List<string>();
        }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Repetir Password")]
        [Compare("Password", ErrorMessage ="Las contraseñas no conciden.")]
        public string ConfirmPassword { get; set;}

        [Required]
        public string NombreUsuario { get; set; }
        [Required]
        [MaxLength(100)]
        public string PrimApellido { get; set; }

        [Required]
        [MaxLength(100)]
        public string SegunApellido { get; set; }
        [Display(Name = "Estado")]
        public bool Activo { get; set; } = true;
        public IList<string> Roles { get; set; }
    }
}
