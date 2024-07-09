using System.ComponentModel.DataAnnotations;

namespace ProyectoTodoFrenosWeb;

	public class EditarRolViewModel
	{
		public string Id { get; set; }
        public EditarRolViewModel()
        {
            Id = string.Empty;
            RolName = string.Empty;
            Usuarios = new List<string>();
        }

        [Required(ErrorMessage ="El nombre del rol es obligatorio")]
        public string RolName { get; set; }
        public List<string> Usuarios { get; set; }
    }

