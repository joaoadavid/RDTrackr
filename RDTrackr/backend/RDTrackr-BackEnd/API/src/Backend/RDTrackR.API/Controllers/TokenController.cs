using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.UseCases.Token;
using RDTrackR.Communication.Requests.Token;
using RDTrackR.Communication.Responses.Token;

namespace RDTrackR.API.Controllers
{
    public class TokenController : RDTrackRBaseController
    {
        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(ResponseTokensJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> RefreshToken(
            [FromServices] IUseRefreshTokenUseCase useCase,
            [FromBody] RequestNewTokenJson request)
        {
            var response = await useCase.Execute(request);
            return Ok(response);
        }
    }
}
