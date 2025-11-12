using MyRecipeBook.Domain.Repositories.User;
using RDTrackR.Domain.Repositories;
using RDTrackR.Domain.Services.Storage;

namespace MyRecipeBook.Application.UseCases.User.Delete.Delete
{
    public class DeleteUserAccountUseCase : IDeleteUserAccountUseCase
    {
        private readonly IUserDeleteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;

        public DeleteUserAccountUseCase(IUserDeleteOnlyRepository repository, IUnitOfWork unitOfWork,IBlobStorageService blobStorageService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _blobStorageService = blobStorageService;
        }
        public async Task Execute(Guid userIdentifier)
        {
            await _blobStorageService.DeleteContainer(userIdentifier);
            await _repository.DeleteAccoutn(userIdentifier);
            await _unitOfWork.Commit();
        }
    }
}
