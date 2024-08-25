using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using ProyectoTodoFrenosWeb.ViewModels;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace ProyectoTodoFrenosWeb.ConsumoServices
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly SmtpSettings _smtpSettings;

        public SmtpEmailSender(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(_smtpSettings.Server)
            {
                Port = _smtpSettings.Port,
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                EnableSsl = _smtpSettings.EnableSsl,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpSettings.FromEmail, _smtpSettings.FromName),
                Subject = subject,
                Body = GetStyledHtmlMessage(htmlMessage),
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);

            await client.SendMailAsync(mailMessage);
        }

        private string GetStyledHtmlMessage(string password)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #e9ecef;
            margin: 0;
            padding: 20px;
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
            width: 100%;
        }}
        .container {{
            width: 100%;
            max-width: 600px;
            background-color: #ffffff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
            border: 2px solid #b7bfc7;
        }}
        .header {{
            text-align: center;
            background-color: #b7bfc7;
            color: #ffffff;
            padding: 10px 0;
            border-radius: 10px 10px 0 0;
        }}
        .header img {{
            width: 100px;
            height: 100px;
            border-radius: 50%;
            border: 4px solid #ffffff;
        }}
        .header h1 {{
            margin: 10px 0;
            font-size: 22px;
            font-weight: 600;
            color: #000000;
        }}
        .content {{
            padding: 20px;
            color: #2c3e50;
            line-height: 1.5;
        }}
        .content h2 {{
            color: #000000;
            font-size: 18px;
            margin-top: 0;
        }}
        .content p {{
            color: #000000;
            font-size: 16px;
        }}
        .card {{
            background-color: #f8f9fa;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
            border: 1px solid #b7bfc7;
            margin-top: 20px;
        }}
        .card h3 {{
            margin-top: 0;
            color: #2c3e50;
        }}
        .button {{
            display: inline-block;
            padding: 10px 15px;
            background-color: #b7bfc7;
            color: #ffffff;
            text-decoration: none;
            border-radius: 5px;
            transition: background-color 0.3s;
            margin-top: 20px;
        }}
        .button:hover {{
            background-color: #8a9299;
        }}
        .footer {{
            text-align: center;
            padding: 15px;
            background-color: #e9ecef;
            color: #2c3e50;
            border-top: 1px solid #ccc;
        }}
        .social-icons {{
            margin-top: 15px;
        }}
        .social-icons a {{
            margin: 0 8px;
            display: inline-block;
            text-decoration: none;
            transition: transform 0.3s, opacity 0.3s;
        }}
        .social-icons a img {{
            width: 35px;
            height: 35px;
            filter: grayscale(100%);
            transition: filter 0.3s;
        }}
        .social-icons a:hover img {{
            filter: grayscale(0%);
        }}
        .social-icons a:hover {{
            transform: scale(1.1);
            opacity: 0.8;
        }}
        /* Estilos responsivos para móviles */
        @media (max-width: 600px) {{
            body {{
                padding: 10px;
            }}
            .container {{
                padding: 15px;
                border-radius: 5px;
            }}
            .header h1 {{
                font-size: 20px;
            }}
            .content h2 {{
                font-size: 16px;
            }}
            .content p {{
                font-size: 14px;
            }}
            .card {{
                padding: 15px;
            }}
            .button {{
                padding: 8px 12px;
                font-size: 14px;
            }}
            .social-icons a img {{
                width: 30px;
                height: 30px;
            }}
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <img src='https://th.bing.com/th/id/OIP.TggtHAalLI6bi33Mj64aTQHaGS?w=560&h=476&rs=1&pid=ImgDetMain' alt='Logo'>
            <h1>ProyectoTodoFrenosWeb</h1>
        </div>
        <div class='content'>
            <h2>Estimado Usuario,</h2>
            <div class='card'>
                <h3>{password}</h3>
            </div>
        </div>
        <div class='footer'>
            <p>¡Gracias por usar nuestro servicio!</p>
            <p>Síguenos en</p>
            <div class='social-icons'>
                <a href='https://www.facebook.com/tallertodofrenos?locale=es_LA' target='_blank'>
                    <img src='https://cdn-icons-png.flaticon.com/512/733/733547.png' alt='Facebook'>
                </a>
                <a href='https://www.instagram.com/tallertodofrenoscr/' target='_blank'>
                    <img src='https://cdn-icons-png.flaticon.com/512/733/733558.png' alt='Instagram'>
                </a>
            </div>
            <p>Equipo de ProyectoTodoFrenosWeb</p>
        </div>
    </div>
</body>
</html>";
        }

    }
}