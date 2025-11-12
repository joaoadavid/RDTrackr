using PostmarkDotNet;
using RDTrackR.Domain.Services.Email;

namespace RDTrackR.Infrastructure.Services.Email
{
    public class PostmarkSendCodeResetPassword : ISendCodeResetPassword
    {
        private readonly PostmarkClient _client;

        public PostmarkSendCodeResetPassword(PostmarkClient client)
        {
            _client = client;
        }

        public async Task SendAsync(string email, string code)
        {
            var message = new PostmarkMessage
            {
                To = email,
                From = "no-reply@myrecipebook.com",
                Subject = "Redefinição de Senha - MyRecipeBook",
                TextBody = $"Olá! Seu código para redefinir a senha é: {code}\n\nEle expira em 1 hora.",
                MessageStream = "outbound"
            };

            await _client.SendMessageAsync(message);
        }
    }
}
