using AutoMapper;
using RDTrackR.Communication.Requests.Recipe;
using RDTrackR.Domain.Entities;
using RDTrackR.Domain.Extensions;
using RDTrackR.Domain.Repositories;
using RDTrackR.Domain.Repositories.Recipe;
using RDTrackR.Domain.Services.LoggedUser;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;

namespace MyRecipeBook.Application.UseCases.Recipe.Update
{
    public class UpdateRecipeUseCase : IUpdateRecipeUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRecipeUpdateOnlyRepository _repository;
        public UpdateRecipeUseCase(ILoggedUser loggedUser, IUnitOfWork unitOfWork, IMapper mapper,IRecipeUpdateOnlyRepository repository)
        {
            _loggedUser = loggedUser;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
        }
        public async Task Execute(long recipeId, RequestRecipeJson request)
        {
            Validate(request);

            var loggedUser = await _loggedUser.User();

            var recipe = await _repository.GetById(loggedUser, recipeId);

            if (recipe is null)
                throw new NotFoundException(ResourceMessagesException.RECIPE_NOT_FOUND);

            recipe.Ingredients.Clear();
            recipe.Instructions.Clear();
            recipe.DishTypes.Clear();

            _mapper.Map(request, recipe);

            var instructions = request.Instructions.OrderBy(i=>i.Step).ToList();
            for (var index = 0; index<instructions.Count;index++)
                instructions[index].Step = index + 1;

            recipe.Instructions = _mapper.Map<List<Instruction>>(instructions);

            _repository.Update(recipe);

            await _unitOfWork.Commit();
        }

        private void Validate(RequestRecipeJson request)
        {
            var result = new RecipeValidator().Validate(request);

            if (result.IsValid.IsFalse())
            {
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
            }
        }
    }
}
