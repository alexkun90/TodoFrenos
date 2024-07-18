using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProyectoTodoFrenosWeb.ViewModels
{
    public class CrearRolViewModel
    {
        [Required(ErrorMessage ="Este campo es obligatorio")]
        [Display(Name= "Rol")]
        public string NameRol { get; set; }
    }
}
