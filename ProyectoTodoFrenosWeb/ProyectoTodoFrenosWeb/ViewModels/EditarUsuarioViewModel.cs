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
        [EmailAddress]
        public string Email { get; set; }

        public List<string> Notificaciones { get; set; }
        public IList<string> Roles { get; set;}
    }
}
