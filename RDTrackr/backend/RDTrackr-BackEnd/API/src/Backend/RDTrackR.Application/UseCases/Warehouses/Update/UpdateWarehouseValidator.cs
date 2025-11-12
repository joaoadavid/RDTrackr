using FluentValidation;
using RDTrackR.Communication.Requests.Warehouse;
using RDTrackR.Exceptions;

namespace RDTrackR.Application.UseCases.Warehouses.Update
{
    public class UpdateWarehouseValidator : AbstractValidator<RequestUpdateWarehouseJson>
    {
        public UpdateWarehouseValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ResourceMessagesException.WAREHOUSE_NAME_REQUIRED);

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage(ResourceMessagesException.WAREHOUSE_LOCATION_REQUIRED);

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage(ResourceMessagesException.WAREHOUSE_CAPACITY_INVALID);

            RuleFor(x => x.Items)
                .GreaterThanOrEqualTo(0).WithMessage(ResourceMessagesException.WAREHOUSE_ITEMS_INVALID);
        }
    }
}
