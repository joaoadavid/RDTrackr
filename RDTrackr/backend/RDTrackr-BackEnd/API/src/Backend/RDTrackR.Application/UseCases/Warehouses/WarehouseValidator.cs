using FluentValidation;
using RDTrackR.Communication.Requests.Warehouse;
using RDTrackR.Exceptions;

namespace RDTrackR.Application.UseCases.Warehouses
{
    public class WarehouseValidator : AbstractValidator<RequestRegisterWarehouseJson>
    {
        public WarehouseValidator()
        {
            RuleFor(w => w.Name)
                .NotEmpty().WithMessage(ResourceMessagesException.WAREHOUSE_NAME_REQUIRED);

            RuleFor(w => w.Location)
                .NotEmpty().WithMessage(ResourceMessagesException.WAREHOUSE_LOCATION_REQUIRED);

            RuleFor(w => w.Capacity)
                .GreaterThan(0).WithMessage(ResourceMessagesException.WAREHOUSE_CAPACITY_INVALID);

            RuleFor(w => w.Items)
                .GreaterThanOrEqualTo(0).WithMessage(ResourceMessagesException.WAREHOUSE_ITEMS_INVALID);
        }
    }
}
