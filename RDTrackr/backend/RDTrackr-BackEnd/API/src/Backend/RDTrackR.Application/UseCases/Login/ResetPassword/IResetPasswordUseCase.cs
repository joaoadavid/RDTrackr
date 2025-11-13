using RDTrackR.Communication.Requests.Password;

namespace RDTrackR.Application.UseCases.Login.ResetPassword
{
    public interface IResetPasswordUseCase
    {
        Task Execute(RequestResetYourPasswordJson request);
    }
}
