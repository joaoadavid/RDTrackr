
namespace RDTrackR.Application.UseCases.User.Admin
{
    public interface IAdminToggleUserActiveUseCase
    {
        Task Execute(long id);
    }
}