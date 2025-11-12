using FluentValidation;
using RDTrackR.Communication.Requests.User;
using RDTrackR.Exceptions;

namespace RDTrackR.Application.UseCases.Users.Validators
{
    public class AdminUpdateUserValidator : AbstractValidator<RequestAdminUpdateUserJson>
    {
        public AdminUpdateUserValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage(ResourceMessagesException.EMAIL_REQUIRED)
                .EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);
        }
    }
}
