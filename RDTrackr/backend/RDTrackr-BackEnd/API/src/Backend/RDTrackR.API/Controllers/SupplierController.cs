using Microsoft.AspNetCore.Mvc;
using RDTrackR.API.Attributes;
using RDTrackR.Application.UseCases.Suppliers.Register;
using RDTrackR.Application.UseCases.Suppliers.GetAll;
using RDTrackR.Application.UseCases.Suppliers.Update;
using RDTrackR.Application.UseCases.Suppliers.Delete;
using RDTrackR.Communication.Requests.Supplier;
using RDTrackR.Communication.Responses.Supplier;
using RDTrackR.Communication.Responses.Error;

namespace RDTrackR.API.Controllers
{
    [AuthenticatedUser]
    public class SupplierController : RDTrackRBaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseSupplierJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterSupplierUseCase useCase,
            [FromBody] RequestRegisterSupplierJson request)
        {
            var result = await useCase.Execute(request);
            return Created(string.Empty, result);
        }

        // GET /suppliers
        [HttpGet]
        [ProducesResponseType(typeof(List<ResponseSupplierJson>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromServices] IGetAllSuppliersUseCase useCase)
        {
            var result = await useCase.Execute();
            return Ok(result);
        }

        // PUT /suppliers/{id}
        [HttpPut("{id:long}")]
        [ProducesResponseType(typeof(ResponseSupplierJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [FromServices] IUpdateSupplierUseCase useCase,
            [FromRoute] long id,
            [FromBody] RequestUpdateSupplierJson request)
        {
            var result = await useCase.Execute(id, request);
            return Ok(result);
        }

        // DELETE /suppliers/{id}
        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(
            [FromServices] IDeleteSupplierUseCase useCase,
            [FromRoute] long id)
        {
            await useCase.Execute(id);
            return NoContent();
        }
    }
}
