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

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [MaxLength(100)]
        public string PrimApellido { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [MaxLength(100)]
        public string SegunApellido { get; set; }
        [Display(Name = "Estado")]
        public bool Activo { get; set; } = true;

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [EmailAddress]
        public string Email { get; set; }

        public List<string> Notificaciones { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public IList<string> Roles { get; set; }
    }
}