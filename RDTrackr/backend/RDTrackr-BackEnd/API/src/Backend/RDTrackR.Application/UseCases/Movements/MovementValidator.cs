using FluentValidation;
using RDTrackR.Communication.Requests.Movements;
using RDTrackR.Exceptions;

namespace RDTrackR.Application.UseCases.Movements
{
    public class MovementValidator : AbstractValidator<RequestRegisterMovementJson>
    {
        public MovementValidator()
        {
            RuleFor(m => m.Reference)
                .NotEmpty()
                .WithMessage(ResourceMessagesException.MOVEMENT_REFERENCE_REQUIRED);

            RuleFor(m => m.ProductId)
                .GreaterThan(0)
                .WithMessage(ResourceMessagesException.MOVEMENT_PRODUCT_REQUIRED);

            RuleFor(m => m.WarehouseId)
                .GreaterThan(0)
                .WithMessage(ResourceMessagesException.MOVEMENT_WAREHOUSE_REQUIRED);

            RuleFor(m => m.Quantity)
                .GreaterThan(0)
                .WithMessage(ResourceMessagesException.MOVEMENT_QUANTITY_INVALID);

            RuleFor(m => m.Type)
                .IsInEnum()
                .WithMessage(ResourceMessagesException.MOVEMENT_TYPE_INVALID);
        }
    }

}
