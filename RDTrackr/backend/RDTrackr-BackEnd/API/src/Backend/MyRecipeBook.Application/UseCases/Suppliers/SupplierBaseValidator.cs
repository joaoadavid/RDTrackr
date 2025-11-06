using FluentValidation;
using RDTrackR.Communication.Requests.Supplier;
using RDTrackR.Exceptions;

namespace RDTrackR.Application.UseCases.Suppliers
{
    public class SupplierBaseValidator : AbstractValidator<RequestBaseSupplierJson>
    {
        public SupplierBaseValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty()
                .WithMessage(ResourceMessagesException.SUPPLIER_NAME_REQUIRED);

            RuleFor(s => s.Email)
                .NotEmpty()
                .WithMessage(ResourceMessagesException.SUPPLIER_EMAIL_REQUIRED)
                .EmailAddress()
                .WithMessage(ResourceMessagesException.SUPPLIER_EMAIL_INVALID);
        }
    }


}
