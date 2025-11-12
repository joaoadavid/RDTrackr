using RDTrackR.Application.UseCases.PurchaseOrders.Register;
using RDTrackR.Communication.Requests.PurchaseOrders;
using RDTrackR.Communication.Requests.Replenishment;
using RDTrackR.Communication.Responses.PurchaseOrders;
using RDTrackR.Exceptions.ExceptionBase;

namespace RDTrackR.Application.UseCases.Replenishment.Register
{
    public class GeneratePoFromReplenishmentUseCase : IGeneratePoFromReplenishmentUseCase
    {
        private readonly IRegisterPurchaseOrderUseCase _createPo;

        public GeneratePoFromReplenishmentUseCase(IRegisterPurchaseOrderUseCase createPo)
        {
            _createPo = createPo;
        }

        public async Task<ResponsePurchaseOrderJson> Execute(RequestGeneratePoFromReplenishmentJson request)
        {
            var validator = new GenerateReplenishmentValidator();
            var result = await validator.ValidateAsync(request);

            if (!result.IsValid)
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());

            var poRequest = new RequestCreatePurchaseOrderJson
            {
                SupplierId = request.SupplierId,
                Items = request.Items.Select(i => new RequestCreatePurchaseOrderItemJson
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            return await _createPo.Execute(poRequest);
        }

    }

}
