using FluentValidation;
using RDTrackR.Communication.Requests.Replenishment;
using RDTrackR.Exceptions;

namespace RDTrackR.Application.UseCases.Replenishment.Register
{
    public class GenerateReplenishmentValidator : AbstractValidator<RequestGeneratePoFromReplenishmentJson>
    {
        public GenerateReplenishmentValidator()
        {
            RuleFor(r => r.SupplierId)
                .GreaterThan(0)
                .WithMessage(ResourceMessagesException.SUPPLIER_NOT_FOUND);

            RuleFor(r => r.Items)
                .NotEmpty()
                .WithMessage(ResourceMessagesException.REPLENISHMENT_ITEMS_REQUIRED);

            RuleForEach(r => r.Items).ChildRules(items =>
            {
                items.RuleFor(i => i.ProductId)
                    .GreaterThan(0)
                    .WithMessage(ResourceMessagesException.PRODUCT_NOT_FOUND);

                items.RuleFor(i => i.Quantity)
                    .GreaterThan(0)
                    .WithMessage(ResourceMessagesException.REPLENISHMENT_QTY_INVALID);

                items.RuleFor(i => i.UnitPrice)
                    .GreaterThan(0)
                    .WithMessage(ResourceMessagesException.REPLENISHMENT_UNITPRICE_INVALID);
            });
        }
    }
}
