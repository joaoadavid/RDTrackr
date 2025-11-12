using FluentValidation;
using RDTrackR.Application.SharedValidators;
using RDTrackR.Communication.Requests.Password;

namespace RDTrackR.Application.UseCases.User.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<RequestChangePasswordJson>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator<RequestChangePasswordJson>());
        }
    }
}
