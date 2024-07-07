﻿using Microsoft.AspNetCore.Identity;

namespace ProyectoTodoFrenosWeb.ViewModel
{
    public class UsuarioViewModel : IdentityUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
    }
}
