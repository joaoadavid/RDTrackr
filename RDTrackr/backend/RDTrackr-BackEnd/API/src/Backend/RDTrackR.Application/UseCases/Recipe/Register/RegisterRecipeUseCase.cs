using AutoMapper;
using MyRecipeBook.Application.UseCases.Recipe.Register;
using RDTrackR.Application.Extensions;
using RDTrackR.Communication.Requests.Recipe;
using RDTrackR.Communication.Requests.User;
using RDTrackR.Communication.Responses.Recipe;
using RDTrackR.Domain.Entities;
using RDTrackR.Domain.Extensions;
using RDTrackR.Domain.Repositories;
using RDTrackR.Domain.Repositories.Recipe;
using RDTrackR.Domain.Services.LoggedUser;
using RDTrackR.Domain.Services.Storage;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;

namespace MyRecipeBook.Application.UseCases.Recipe
{
    public class RegisterRecipeUseCase : IRegisterRecipeUseCase
    {
        private readonly IRecipeWriteOnlyRepository _repository;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unityOfWork;
        private readonly IMapper _mapper;
        private readonly IBlobStorageService _blobStorageService;

        public RegisterRecipeUseCase(
            IRecipeWriteOnlyRepository repository,
            ILoggedUser loggedUser,
            IUnitOfWork unityOfWork,
            IBlobStorageService blobStorageService,
            IMapper mapper)
        {
            _repository = repository;
            _loggedUser = loggedUser;
            _unityOfWork = unityOfWork;
            _blobStorageService = blobStorageService;
            _mapper = mapper;
        }

        public async Task<ResponseRegisteredRecipeJson> Execute(RequestRegisterRecipeFormData request)
        {
            Validate(request);

            var loggedUser = await _loggedUser.User();

            var recipe = _mapper.Map<RDTrackR.Domain.Entities.Recipe>(request);
            recipe.UserId = loggedUser.Id;

            var instructions = request.Instructions.OrderBy(i => i.Step).ToList();
            for (var index = 0; index < instructions.Count; index++)
                instructions[index].Step = index + 1;

            recipe.Instructions = _mapper.Map<IList<Instruction>>(instructions);

            if (request.Image is not null)
            {
                var fileStream = request.Image.OpenReadStream();

                (var isValidImage, var extension) = fileStream.ValidateAndGetImageExtension();

                if (isValidImage.IsFalse())
                {
                    throw new ErrorOnValidationException([ResourceMessagesException.ONLY_IMAGES_ACCEPTED]);
                }

                recipe.ImageIdentifier = $"{Guid.NewGuid()}{extension}";

                await _blobStorageService.Upload(loggedUser, fileStream, recipe.ImageIdentifier);
            }

            await _repository.Add(recipe);

            await _unityOfWork.Commit();

            return _mapper.Map<ResponseRegisteredRecipeJson>(recipe);
        }

        private static void Validate(RequestRecipeJson request)
        {
            var result = new RecipeValidator().Validate(request);

            if (result.IsValid.IsFalse())
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());

        }
    }
}
