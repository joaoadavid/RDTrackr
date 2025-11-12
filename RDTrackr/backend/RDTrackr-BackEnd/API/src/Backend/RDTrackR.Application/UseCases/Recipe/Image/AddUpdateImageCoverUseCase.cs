using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using Microsoft.AspNetCore.Http;
using RDTrackR.Domain.Extensions;
using RDTrackR.Domain.Repositories;
using RDTrackR.Domain.Repositories.Recipe;
using RDTrackR.Domain.Services.LoggedUser;
using RDTrackR.Domain.Services.Storage;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;

namespace MyRecipeBook.Application.UseCases.Recipe.Image
{
    public class AddUpdateImageCoverUseCase : IAddUpdateImageCoverUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IRecipeUpdateOnlyRepository _repository;
        private readonly IUnitOfWork _unityOfWork;
        private readonly IBlobStorageService _blobStorageService;

        public AddUpdateImageCoverUseCase(ILoggedUser loggedUser, IRecipeUpdateOnlyRepository repository, IUnitOfWork unitOfWork, IBlobStorageService blobStorageService)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _unityOfWork = unitOfWork;
            _blobStorageService = blobStorageService;
        }

        public async Task Execute(long id, IFormFile file)
        {
            var loggedUser = await _loggedUser.User();

            var recipe = await _repository.GetById(loggedUser, id);

            if (recipe is null)
                throw new NotFoundException(ResourceMessagesException.RECIPE_NOT_FOUND);

            var fileStream = file.OpenReadStream();

            if(fileStream.Is<PortableNetworkGraphic>().IsFalse() &&
                fileStream.Is<JointPhotographicExpertsGroup>().IsFalse())
            {
                throw new ErrorOnValidationException([ResourceMessagesException.ONLY_IMAGES_ACCEPTED]);
            }

            if (string.IsNullOrEmpty(recipe.ImageIdentifier))
            {
                recipe.ImageIdentifier = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

                _repository.Update(recipe);

                await _unityOfWork.Commit();
            }

            fileStream.Position = 0;

            await _blobStorageService.Upload(loggedUser, fileStream,recipe.ImageIdentifier);
        }
    }
}
