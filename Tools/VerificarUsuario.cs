using Domain.Entities;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace TEAyudo_Usuarios;

public class VerificacionRegistro
{
    public static async Task EnviarToken(Usuario usuario)
    {
        var apiKeyMitad1 = "SG.6agBuaYXTv6N1EcfBeJ3LQ.";
        var apiKeyMitad2 = "SxcaYKwbrco6oqZjDdEk7nBM1l-WElTeC4lcZ1kY50k";
        var apiKey = apiKeyMitad1 + apiKeyMitad2;
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("teayudo@yopmail.com", "TEAyudo");
        var subject = "Verificación de Registro - TEAyudo";
        var to = new EmailAddress(usuario.CorreoElectronico, usuario.Nombre);
        var contenidoHtml = $@"
            <html>
            <head>
                <style>
                    /* Estilos CSS aquí */
                    body {{
                        font-family: Arial, sans-serif;
                    }}
                    .enlace {{
                        color: #007bff;
                        text-decoration: none;
                    }}
                    .enlace:hover {{
                        text-decoration: underline;
                    }}
                </style>
            </head>
            <body>
                <p>Hola {usuario.Nombre} {usuario.Apellido},</p>
                <p>Le damos la bienvenida a TEAyudo. Por favor, haga clic en el siguiente enlace para confirmar su registro:</p>
                <p><a class='enlace' href='https://localhost:7152/api/Verificacion/VerificarCorreo?token={usuario.Token}'>Confirmar Registro</a></p>
                <p>Si no se ha registrado en TEAyudo, puede ignorar este correo electrónico.</p>
            </body>
            </html>
        ";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, null, contenidoHtml);
        var response = await client.SendEmailAsync(msg);
    }
}
