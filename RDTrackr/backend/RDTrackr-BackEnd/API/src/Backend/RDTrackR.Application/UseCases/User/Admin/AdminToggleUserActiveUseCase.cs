using RDTrackR.Domain.Repositories.Users;
using RDTrackR.Domain.Repositories;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;

namespace RDTrackR.Application.UseCases.User.Admin
{
    public class AdminToggleUserActiveUseCase : IAdminToggleUserActiveUseCase
    {
        private readonly IUserReadOnlyRepository _readRepository;
        private readonly IUserWriteOnlyRepository _writeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AdminToggleUserActiveUseCase(
            IUserReadOnlyRepository readRepository,
            IUserWriteOnlyRepository writeRepository,
            IUnitOfWork unitOfWork)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long id)
        {
            var user = await _readRepository.GetByIdAsync(id)
                ?? throw new NotFoundException(ResourceMessagesException.USER_NOT_FOUND);

            user.Active = !user.Active;

            await _writeRepository.UpdateAsync(user);
            await _unitOfWork.Commit();
        }
    }
}
