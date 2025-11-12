using MyRecipeBook.Domain.Repositories.Token;
using RDTrackR.Domain.Repositories;
using RDTrackR.Exceptions.ExceptionBase.Token;

namespace RDTrackR.Application.UseCases.Login.Logout
{
    public class LogoutUseCase : ILogoutUseCase
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LogoutUseCase(
            ITokenRepository tokenRepository,
            IUnitOfWork unitOfWork)
        {
            _tokenRepository = tokenRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(string refreshToken)
        {
            var token = await _tokenRepository.Get(refreshToken);

            if (token is null)
                throw new RefreshTokenNotFoundException();

            token.IsRevoked = true;

            await _unitOfWork.Commit();
        }
    }
}
