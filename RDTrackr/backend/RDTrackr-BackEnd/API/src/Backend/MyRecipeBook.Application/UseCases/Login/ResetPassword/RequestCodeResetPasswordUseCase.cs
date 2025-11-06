using MyRecipeBook.Domain.Repositories.User;
using RDTrackR.Domain.Entities;
using RDTrackR.Domain.Repositories;
using RDTrackR.Domain.Repositories.Password;
using RDTrackR.Domain.Security.Cryptography;
using RDTrackR.Domain.Services.Email;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;

namespace MyRecipeBook.Application.UseCases.Login.ResetPassword
{
    public class RequestCodeResetPasswordUseCase : IRequestCodeResetPasswordUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly ICodeToPerformActionRepository _codeRepository;
        private readonly ISendCodeResetPassword _emailSender;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICodeGenerator _codeGenerator;

        public RequestCodeResetPasswordUseCase(
            IUserReadOnlyRepository userReadOnlyRepository,
            ICodeToPerformActionRepository codeRepository,
            ISendCodeResetPassword emailSender,
            IUnitOfWork unitOfWork,
            ICodeGenerator codeGenerator)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _codeRepository = codeRepository;
            _emailSender = emailSender;
            _unitOfWork = unitOfWork;
            _codeGenerator = codeGenerator;
        }

        public async Task Execute(string email)
        {
            var user = await _userReadOnlyRepository.GetByEmail(email);

            if (user is null)
                throw new ErrorOnValidationException([ResourceMessagesException.USER_NOT_FOUND]);

            var codeRandom = _codeGenerator.Generate(6);

            _codeRepository.DeleteAllUserCodes(user);

            var codeToPerformAction = new CodeToPerformAction
            {
                Value = codeRandom,
                UserId = user.Id,
                CreatedOn = DateTime.UtcNow
            };

            await _codeRepository.Add(codeToPerformAction);

            await _unitOfWork.Commit();

            await _emailSender.SendAsync(user.Email, codeRandom);
        }
    }
}
