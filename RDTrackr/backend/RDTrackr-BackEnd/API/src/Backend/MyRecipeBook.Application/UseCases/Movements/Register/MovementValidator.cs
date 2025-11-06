using FluentValidation;
using RDTrackR.Communication.Requests.Movements;
using RDTrackR.Exceptions;

namespace RDTrackR.Application.UseCases.Movements.Register
{
    public class MovementValidator : AbstractValidator<RequestRegisterMovementJson>
    {
        public MovementValidator()
        {
            RuleFor(x => x.Reference)
                .NotEmpty().WithMessage(ResourceMessagesException.MOVEMENT_REFERENCE_REQUIRED);

            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage(ResourceMessagesException.MOVEMENT_PRODUCT_REQUIRED);

            RuleFor(x => x.WarehouseId)
                .GreaterThan(0).WithMessage(ResourceMessagesException.MOVEMENT_WAREHOUSE_REQUIRED);

            RuleFor(x => x.Type).IsInEnum()
                .WithMessage(ResourceMessagesException.MOVEMENT_TYPE_INVALID);

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage(ResourceMessagesException.MOVEMENT_QUANTITY_INVALID);
        }       
    }
}
