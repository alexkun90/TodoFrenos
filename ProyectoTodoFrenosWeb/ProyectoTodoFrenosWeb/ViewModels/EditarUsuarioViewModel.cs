using System.ComponentModel.DataAnnotations;

namespace ProyectoTodoFrenosWeb.ViewModels
{
    public class EditarUsuarioViewModel
    {
        public EditarUsuarioViewModel()
        {
            Notificaciones = new List<string>();
            Roles = new List<string>();
        }

        public string Id { get; set; }
        
        [Required]
        public string NombreUsuario { get; set; }
        [Required]
        [MaxLength(100)]
        public string PrimApellido { get; set; }

        [Required]
        [MaxLength(100)]
        public string SegunApellido { get; set; }
        [Display(Name ="Estado")]
        public bool Activo { get; set; } = true;

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public List<string> Notificaciones { get; set; }
        public IList<string> Roles { get; set;}
    }
}
