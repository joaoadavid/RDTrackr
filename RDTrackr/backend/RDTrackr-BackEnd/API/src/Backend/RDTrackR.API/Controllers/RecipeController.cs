using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.UseCases.Recipe.Delete;
using MyRecipeBook.Application.UseCases.Recipe.Filter;
using MyRecipeBook.Application.UseCases.Recipe.GetById;
using MyRecipeBook.Application.UseCases.Recipe.Image;
using MyRecipeBook.Application.UseCases.Recipe.Register;
using MyRecipeBook.Application.UseCases.Recipe.Update;
using MyRecipeBook.Communication.Responses;
using RDTrackR.API.Attributes;
using RDTrackR.API.Binders;
using RDTrackR.Communication.Requests.Recipe;
using RDTrackR.Communication.Requests.User;
using RDTrackR.Communication.Responses.Error;
using RDTrackR.Communication.Responses.Recipe;

namespace RDTrackR.API.Controllers
{
    [AuthenticatedUser]
    public class RecipeController : RDTrackRBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredRecipeJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterRecipeUseCase useCase,
            [FromForm] RequestRegisterRecipeFormData request)
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }

        [HttpPost("filter")]
        [ProducesResponseType(typeof(ResponseRecipesJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Filter(
         [FromServices] IFilterRecipeUseCase useCase,
         [FromBody] RequestFilterRecipeJson request)
        {
            var response = await useCase.Execute(request);

            if (response.Recipes.Any())
                return Ok(response);

            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseRecipeJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
            [FromServices] IGetRecipeByIdUseCase useCase,
            [FromRoute][ModelBinder(typeof(RDTrackRBinder))] long id)
        {
            var response = await useCase.Execute(id);
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
            [FromServices] IDeleteRecipeUseCase useCase,
            [FromRoute][ModelBinder(typeof(RDTrackRBinder))] long id)
        {
            await useCase.Execute(id);
            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            [FromServices] IUpdateRecipeUseCase useCase,
            [FromRoute][ModelBinder(typeof(RDTrackRBinder))] long id,
            [FromBody] RequestRecipeJson request)
        {
            await useCase.Execute(id, request);
            return NoContent();
        }

        [HttpPut]
        [Route("image/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateImage(
            [FromServices] IAddUpdateImageCoverUseCase useCase,
            [FromRoute][ModelBinder(typeof(RDTrackRBinder))] long id,
            IFormFile file)
        {
            await useCase.Execute(id, file);
            return NoContent();
        }
    }
}
