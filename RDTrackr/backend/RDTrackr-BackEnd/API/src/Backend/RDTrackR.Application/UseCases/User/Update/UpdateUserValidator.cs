using FluentValidation;
using RDTrackR.Communication.Requests.User;
using RDTrackR.Domain.Extensions;
using RDTrackR.Exceptions;
namespace RDTrackR.Application.UseCases.User.Update
{
    public class UpdateUserValidator : AbstractValidator<RequestUpdateUserJson>
    {
        public UpdateUserValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
            RuleFor(request => request.Email).NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY);

            When(request => string.IsNullOrWhiteSpace(request.Email).IsFalse(), () =>
            {
                RuleFor(request => request.Email).EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);
            });
        }
    }
}
