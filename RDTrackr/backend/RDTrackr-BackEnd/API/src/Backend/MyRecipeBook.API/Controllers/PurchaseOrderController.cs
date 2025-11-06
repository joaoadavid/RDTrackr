using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RDTrackR.API.Attributes;
using RDTrackR.Application.UseCases.PurchaseOrders.Delete;
using RDTrackR.Application.UseCases.PurchaseOrders.GetAll;
using RDTrackR.Application.UseCases.PurchaseOrders.Register;
using RDTrackR.Application.UseCases.PurchaseOrders.Update;
using RDTrackR.Communication.Requests.PurchaseOrders;

namespace RDTrackR.API.Controllers
{
    [AuthenticatedUser]
    public class PurchaseOrderController : RDTrackRBaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromServices] IRegisterPurchaseOrderUseCase useCase,
            [FromBody] RequestCreatePurchaseOrderJson request)
            => Created("", await useCase.Execute(request));

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromServices] IGetPurchaseOrdersUseCase useCase)
            => Ok(await useCase.Execute());

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
