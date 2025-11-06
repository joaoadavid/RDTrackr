using RDTrackR.Communication.Requests.Password;

namespace MyRecipeBook.Application.UseCases.User.ChangePassword
{
    public interface IChangePasswordUseCase
    {
        Task Execute(RequestChangePasswordJson request);
    }
}