namespace RDTrackR.Domain.Services.Email
{
    public interface ISendCodeResetPassword
    {
        /// <summary>
        /// Envia um código de redefinição de senha para o e-mail do usuário.
        /// </summary>
        /// <param name="email">Endereço de e-mail do destinatário.</param>
        /// <param name="code">Código de verificação gerado.</param>
        Task SendAsync(string email, string code);
    }
}
