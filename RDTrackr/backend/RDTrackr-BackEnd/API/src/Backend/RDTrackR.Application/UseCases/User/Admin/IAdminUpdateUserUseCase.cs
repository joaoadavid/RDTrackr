using RDTrackR.Communication.Requests.User;

namespace RDTrackR.Application.UseCases.User.Admin
{
    public interface IAdminUpdateUserUseCase
    {
        Task Execute(long id, RequestAdminUpdateUserJson request);
    }
}