using MimeKit;
using MailKit.Net.Smtp;
using System.Net.Mail;

namespace TestUAA2.Services
{
    public class EmailService: IEmailService
    {
        private readonly IConfiguration _config; // Accès à la configuration (appsettings.json)
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendAsync(string to, string subject, string body)
        {
            // Lecture des paramètres SMTP depuis la configuration
            var host = _config["SmtpSettings:Server"];
            //var port = int.Parse(_config["SmtpSettings:Port"]);
            var port = int.TryParse(_config["SmtpSettings:Port"], out var p) ? p : 25;
            var from = _config["SmtpSettings:SenderEmail"];

            if (string.IsNullOrEmpty(host))
                throw new Exception("SMTP host not configured");
            // 1. Création du message avec MimeKit
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Newsletter API", from));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;

            // Corps du mail (HTML autorisé)
            message.Body = new TextPart("html") { Text = body };

            // 2. Envoi avec MailKit
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                // Pour Smtp4Dev, on désactive le SSL (false)  Connexion au serveur SMTP 
                await client.ConnectAsync(host, port, MailKit.Security.SecureSocketOptions.None);

                // Envoi du message
                await client.SendAsync(message);
                // Déconnexion propre
                await client.DisconnectAsync(true);
            }
            /*
            using var client = new SmtpClient(host, port);
            var message = new MailMessage(from, to, subject, body) { IsBodyHtml = true };
            await client.SendMailAsync(message);*/
        }
    }
}
