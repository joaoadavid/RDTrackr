
namespace MyRecipeBook.Application.UseCases.Login.ResetPassword
{
    public interface IRequestCodeResetPasswordUseCase
    {
        Task Execute(string email);
    }
}