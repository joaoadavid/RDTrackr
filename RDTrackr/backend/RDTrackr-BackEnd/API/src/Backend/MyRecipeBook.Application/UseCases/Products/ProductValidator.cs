using FluentValidation;
using RDTrackR.Communication.Requests.Product;
using RDTrackR.Exceptions;

namespace RDTrackR.Application.UseCases.Products
{
    public class ProductValidator : AbstractValidator<RequestRegisterProductJson>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Sku)
                .NotEmpty().WithMessage(ResourceMessagesException.PRODUCT_SKU_REQUIRED)
                .MaximumLength(50);

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(ResourceMessagesException.PRODUCT_NAME_REQUIRED)
                .MaximumLength(255);

            RuleFor(p => p.Category)
                .NotEmpty().WithMessage(ResourceMessagesException.PRODUCT_CATEGORY_REQUIRED);

            RuleFor(p => p.UoM)
                .NotEmpty().WithMessage(ResourceMessagesException.PRODUCT_UNIT_REQUIRED);

            RuleFor(p => p.Price)
                .GreaterThanOrEqualTo(0).WithMessage(ResourceMessagesException.PRODUCT_PRICE_INVALID);

            RuleFor(p => p.Stock)
                .GreaterThanOrEqualTo(0).WithMessage(ResourceMessagesException.PRODUCT_STOCK_INVALID);

            RuleFor(p => p.ReorderPoint)
                .GreaterThanOrEqualTo(0).WithMessage(ResourceMessagesException.PRODUCT_REORDER_INVALID);
        }
    }
}
