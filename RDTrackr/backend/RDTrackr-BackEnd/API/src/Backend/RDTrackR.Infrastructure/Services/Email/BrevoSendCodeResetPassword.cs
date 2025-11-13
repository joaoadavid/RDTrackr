using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using RDTrackR.Domain.Services.Email;

namespace RDTrackR.Infrastructure.Services.Email
{
    public class BrevoSendCodeResetPassword : ISendCodeResetPassword
    {
        private readonly IConfiguration _config;

        public BrevoSendCodeResetPassword(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendAsync(string email, string code)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("RDTrackR", _config["Brevo:From"]));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Redefinição de Senha - RDTrackR";

            message.Body = new TextPart("plain")
            {
                Text = $"Olá! Seu código de redefinição é: {code}\n\nEle expira em 1 hora."
            };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp-relay.brevo.com", 587, false);

            await client.AuthenticateAsync(_config["Brevo:User"], _config["Brevo:Password"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}