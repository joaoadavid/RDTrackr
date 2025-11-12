using RDTrackR.Domain.Extensions;
using RDTrackR.Domain.Repositories;
using RDTrackR.Domain.Repositories.Recipe;
using RDTrackR.Domain.Services.LoggedUser;
using RDTrackR.Domain.Services.Storage;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;

namespace MyRecipeBook.Application.UseCases.Recipe.Delete;
public class DeleteRecipeUseCase : IDeleteRecipeUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IRecipeReadOnlyRepository _repositoryRead;
    private readonly IRecipeWriteOnlyRepository _repositoryWrite;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBlobStorageService _blobStorageService;

    public DeleteRecipeUseCase(
        ILoggedUser loggedUser,
        IRecipeReadOnlyRepository repositoryRead,
        IRecipeWriteOnlyRepository repositoryWrite,
        IBlobStorageService blobStorageService,
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _repositoryRead = repositoryRead;
        _repositoryWrite = repositoryWrite;
        _blobStorageService = blobStorageService;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long recipeId)
    {
        var loggedUser = await _loggedUser.User();

        var recipe = await _repositoryRead.GetById(loggedUser, recipeId);

        if (recipe is null)
            throw new NotFoundException(ResourceMessagesException.RECIPE_NOT_FOUND);

        if(recipe.ImageIdentifier.NotEmpty())
            await _blobStorageService.Delete(loggedUser, recipe.ImageIdentifier);

        await _repositoryWrite.Delete(recipeId);

        await _unitOfWork.Commit();
    }
}