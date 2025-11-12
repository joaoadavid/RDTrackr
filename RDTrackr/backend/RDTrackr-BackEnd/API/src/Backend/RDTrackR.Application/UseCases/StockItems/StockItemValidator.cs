using FluentValidation;
using RDTrackR.Communication.Requests.StockItem;
using RDTrackR.Exceptions;

namespace RDTrackR.Application.UseCases.StockItems
{
    public class StockItemValidator : AbstractValidator<RequestRegisterStockItemJson>
    {
        public StockItemValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage(ResourceMessagesException.PRODUCT_NOT_FOUND);

            RuleFor(x => x.WarehouseId)
                .GreaterThan(0).WithMessage(ResourceMessagesException.WAREHOUSE_NOT_FOUND);

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage(ResourceMessagesException.STOCK_QUANTITY_INVALID);
        }
    }
}
