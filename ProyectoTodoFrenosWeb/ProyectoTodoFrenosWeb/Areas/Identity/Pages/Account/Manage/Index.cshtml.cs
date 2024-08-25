using DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

public class IndexModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IndexModel(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public string Username { get; set; }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [Required]
        [Display(Name = "Nombre de Usuario")]
        public string NombreUsuario { get; set; }

        [Required]
        [Display(Name = "Primer Apellido")]
        public string PrimApellido { get; set; }

        [Required]
        [Display(Name = "Segundo Apellido")]
        public string SegunApellido { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }
    }

    private async Task LoadAsync(ApplicationUser user)
    {
        var userName = await _userManager.GetUserNameAsync(user);
        var email = await _userManager.GetEmailAsync(user);
        var primApellido = user.PrimApellido; // Assuming this is a property in ApplicationUser
        var segunApellido = user.SegunApellido; // Assuming this is a property in ApplicationUser

        Username = userName;

        Input = new InputModel
        {
            NombreUsuario = userName,
            Email = email,
            PrimApellido = primApellido,
            SegunApellido = segunApellido
        };
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        await LoadAsync(user);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        if (!ModelState.IsValid)
        {
            await LoadAsync(user);
            return Page();
        }

        // Update Username
        if (Input.NombreUsuario != user.UserName)
        {
            var setUserNameResult = await _userManager.SetUserNameAsync(user, Input.NombreUsuario);
            if (!setUserNameResult.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to set user name.";
                return RedirectToPage();
            }
        }

        // Update Email
        if (Input.Email != user.Email)
        {
            var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
            if (!setEmailResult.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to set email.";
                return RedirectToPage();
            }
        }

        // Update Apellidos
        user.PrimApellido = Input.PrimApellido;
        user.SegunApellido = Input.SegunApellido;
        await _userManager.UpdateAsync(user);

        await _signInManager.RefreshSignInAsync(user);
        StatusMessage = "Tu perfil ha sido actualizado";
        return RedirectToPage();
    }
}