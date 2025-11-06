namespace RDTrackR.Application.UseCases.Login.Logout
{
    public interface ILogoutUseCase
    {
        Task Execute(string refreshToken);
    }
}
