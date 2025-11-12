using RDTrackR.Communication.Requests.Login;
using RDTrackR.Communication.Responses.User;

namespace RDTrackR.Application.UseCases.Login.DoLogin
{
    public interface IDoLoginUseCase
    {
        public Task<ResponseRegisterUserJson> Execute(RequestLoginJson request);
    }
}
