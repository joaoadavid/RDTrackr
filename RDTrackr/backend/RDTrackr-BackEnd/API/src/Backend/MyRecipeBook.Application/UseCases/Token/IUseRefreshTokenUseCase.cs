using RDTrackR.Communication.Requests.Token;
using RDTrackR.Communication.Responses.Token;

namespace MyRecipeBook.Application.UseCases.Token
{
    public interface IUseRefreshTokenUseCase
    {
        Task<ResponseTokensJson> Execute(RequestNewTokenJson request);
    }
}