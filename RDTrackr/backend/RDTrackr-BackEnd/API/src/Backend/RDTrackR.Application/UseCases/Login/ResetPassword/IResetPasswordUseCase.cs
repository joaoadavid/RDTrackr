using RDTrackR.Communication.Requests.Password;

namespace MyRecipeBook.Application.UseCases.Login.ResetPassword
{
    public interface IResetPasswordUseCase
    {
        Task Execute(RequestResetYourPasswordJson request);
    }
}
