using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.UseCases.Recipe.GetById;
using MyRecipeBook.Communication.Responses;
using RDTrackR.API.Attributes;
using RDTrackR.API.Binders;
using RDTrackR.Application.UseCases.PurchaseOrders.Delete;
using RDTrackR.Application.UseCases.PurchaseOrders.GetAll;
using RDTrackR.Application.UseCases.PurchaseOrders.Register;
using RDTrackR.Application.UseCases.PurchaseOrders.Update;
using RDTrackR.Communication.Requests.PurchaseOrders;
using RDTrackR.Communication.Responses.Error;

namespace RDTrackR.API.Controllers
{
    [AuthenticatedUser]
    public class PurchaseOrderController : RDTrackRBaseController
    {
        [HttpPost]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterPurchaseOrderUseCase useCase,
            [FromBody] RequestCreatePurchaseOrderJson request)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }
            

        [HttpGet]
        public async Task<IActionResult> GetAll(
        [FromServices] IGetPurchaseOrdersUseCase useCase)
        {
            var result = await useCase.Execute();
            return Ok(result);
        }
            

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(
            [FromServices] IUpdatePurchaseOrderStatusUseCase useCase,
            long id,
            [FromBody] RequestUpdatePurchaseOrderStatusJson request)
        {
            await useCase.Execute(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromServices] IDeletePurchaseOrderUseCase useCase,
            long id)
        {
            await useCase.Execute(id);
            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseRecipeJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
            [FromServices] IGetByIdPurchaseOrdersUseCase useCase,
            [FromRoute][ModelBinder(typeof(RDTrackRBinder))] long id)
        {
            await useCase.Execute(id);
            return NoContent();
        }

        [HttpPut("{id}/items")]
        public async Task<IActionResult> UpdateItems(
        [FromServices] IUpdatePurchaseOrderItemsUseCase useCase,
        long id,
        [FromBody] RequestUpdatePurchaseOrderItemsJson request)
        {
            await useCase.Execute(id, request);
            return NoContent();
        }

    }
}
