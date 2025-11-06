using Microsoft.AspNetCore.Mvc;
using RDTrackR.API.Attributes;
using RDTrackR.Application.UseCases.Overview.Get;
using RDTrackR.Application.UseCases.Suppliers.Delete;
using RDTrackR.Application.UseCases.Suppliers.Update;
using RDTrackR.Communication.Requests.Supplier;
using RDTrackR.Communication.Responses.Overview;

namespace RDTrackR.API.Controllers
{
    [AuthenticatedUser]
    public class OverviewController : RDTrackRBaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(ResponseOverviewJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(
            [FromServices] IGetOverviewUseCase useCase)
        {
            var result = await useCase.Execute();
            return Ok(result);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(
       long id,
       [FromServices] IUpdateSupplierUseCase useCase,
       [FromBody] RequestUpdateSupplierJson request)
       => Ok(await useCase.Execute(id, request));

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(
            long id,
            [FromServices] IDeleteSupplierUseCase useCase)
        {
            await useCase.Execute(id);
            return NoContent();
        }
    }
}
