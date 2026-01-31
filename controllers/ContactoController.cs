using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using TallerBackend.Models;

namespace TallerBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactoController : ControllerBase
    {
        [HttpPost]
        public IActionResult EnviarContacto(ContactoDto contacto)
        {
            try
            {
                // Configuración SMTP
                var smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(
                        "victorcoco2005@gmail.com", // Tu correo
                        "cqhf ucqb uycj omor"       // Contraseña de aplicación
                    ),
                    EnableSsl = true
                };

                // Creamos el correo
                var mail = new MailMessage
                {
                    From = new MailAddress("victorcoco2005@gmail.com"), // siempre tu correo
                    Subject = $"Nuevo mensaje de {contacto.Nombre}",
                    Body =
                        $"Nombre: {contacto.Nombre}\n" +
                        $"Email del cliente: {contacto.Email}\n\n" +
                        $"{contacto.Mensaje}"
                };

                // Añadimos dirección de destino
                mail.To.Add("victorcoco2005@gmail.com");

                // Opcional: hacer reply-to para responder directamente al cliente
                mail.ReplyToList.Add(new MailAddress(contacto.Email));

                // Enviar correo
                smtp.Send(mail);

                return Ok(new { message = "Mensaje enviado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // mostrar error real para depuración
            }
        }
    }
}
