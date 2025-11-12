using FluentValidation;
using RDTrackR.Communication.Requests.PurchaseOrders;
using RDTrackR.Exceptions;
using System.Linq;

namespace RDTrackR.Application.UseCases.PurchaseOrders
{
    public class PurchaseOrderUpdateValidator : AbstractValidator<RequestUpdatePurchaseOrderItemsJson>
    {
        public PurchaseOrderUpdateValidator()
        {
            RuleFor(po => po.Items)
                .NotEmpty()
                .WithMessage(ResourceMessagesException.PO_ITEMS_REQUIRED);

            RuleForEach(po => po.Items).ChildRules(items =>
            {
                items.RuleFor(i => i.ProductId)
                    .GreaterThan(0)
                    .WithMessage(ResourceMessagesException.PO_ITEM_PRODUCT_REQUIRED);

                items.RuleFor(i => i.Quantity)
                    .GreaterThan(0)
                    .WithMessage(ResourceMessagesException.PO_ITEM_QUANTITY_INVALID);

                items.RuleFor(i => i.UnitPrice)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage(ResourceMessagesException.PO_ITEM_UNITPRICE_INVALID);
            });

            RuleFor(po => po.Items)
                .Must(items => items.Select(i => i.ProductId).Distinct().Count() == items.Count)
                .WithMessage(ResourceMessagesException.PO_ITEM_DUPLICATED);
        }
    }
}
