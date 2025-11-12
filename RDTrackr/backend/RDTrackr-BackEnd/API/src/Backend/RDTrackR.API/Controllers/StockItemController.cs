using Microsoft.AspNetCore.Mvc;
using RDTrackR.Application.UseCases.StockItems.Register;
using RDTrackR.Communication.Requests.StockItem;
using RDTrackR.Communication.Responses.StockItem;
using RDTrackR.Communication.Responses.Error;
using RDTrackR.API.Attributes;
using RDTrackR.Application.UseCases.StockItems.GetAll;

namespace RDTrackR.API.Controllers
{
    [AuthenticatedUser]
    public class StockItemController : RDTrackRBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseStockItemJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterStockItemUseCase useCase,
            [FromBody] RequestRegisterStockItemJson request)
        {
            var result = await useCase.Execute(request);
            return Created(string.Empty, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ResponseStockItemJson>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromServices] IGetAllStockItemsUseCase useCase)
        {
            var result = await useCase.Execute();
            return Ok(result);
        }
    }
}
