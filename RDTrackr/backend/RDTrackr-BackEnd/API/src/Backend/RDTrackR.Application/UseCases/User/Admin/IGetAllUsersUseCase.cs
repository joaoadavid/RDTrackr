using RDTrackR.Communication.Responses.User.Admin;

namespace RDTrackR.Application.UseCases.User.Admin
{
    public interface IGetAllUsersUseCase
    {
        Task<List<ResponseUserListItemJson>> Execute();
    }
}