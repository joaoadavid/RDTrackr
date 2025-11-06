using Microsoft.AspNetCore.Mvc;
using RDTrackR.API.Attributes;
using RDTrackR.Application.UseCases.Dashboard;
using RDTrackR.Communication.Responses.Recipe;

namespace RDTrackR.API.Controllers
{
    [AuthenticatedUser]
    public class DashboardController : RDTrackRBaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRecipesJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get([FromServices] IGetDashboardUseCase useCase)
        {
            var response = await useCase.Execute();
            if (response.Recipes.Any())
                return Ok(response);
            return NoContent();
        }
    }
}
