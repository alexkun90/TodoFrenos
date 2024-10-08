﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace ProyectoTodoFrenosWeb.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null )
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                var newPassword = GenerateRandomPassword();
                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

                if (!result.Succeeded)
                {
                    // Manejar el error (puedes agregar lógica de manejo de errores aquí)
                    ModelState.AddModelError(string.Empty, "Error al restablecer la contraseña.");
                    return Page();
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code, email = Input.Email },
                    protocol: Request.Scheme);

                var emailSubject = "Cambiar de Contraseña";
                var emailMessage = $"Para cambiar su contraseña acceda <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Aquí</a>. " + "\n"+
                                   $"Su contraseña temporal es: <strong>{newPassword}</strong>";
                await _emailSender.SendEmailAsync(Input.Email, emailSubject, emailMessage);

                /*await _emailSender.SendEmailAsync(
                     Input.Email,
                     "Reset Password",
                     $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");*/

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }

        private string GenerateRandomPassword()
        {
            const int length = 12;
            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";

            // Conjunto de caracteres válidos
            var allChars = upperChars + lowerChars + numbers;

            var random = new Random();
            var newPassword = new char[length];

            // Asegúrate de que la contraseña tenga al menos un carácter de cada tipo
            newPassword[0] = upperChars[random.Next(upperChars.Length)];
            newPassword[1] = lowerChars[random.Next(lowerChars.Length)];
            newPassword[2] = numbers[random.Next(numbers.Length)];

            // Rellena el resto de la contraseña con caracteres aleatorios
            for (int i = 3; i < length; i++)
            {
                newPassword[i] = allChars[random.Next(allChars.Length)];
            }

            // Mezcla los caracteres para asegurar una distribución aleatoria
            return new string(newPassword.OrderBy(c => random.Next()).ToArray());
        }
    }
}
