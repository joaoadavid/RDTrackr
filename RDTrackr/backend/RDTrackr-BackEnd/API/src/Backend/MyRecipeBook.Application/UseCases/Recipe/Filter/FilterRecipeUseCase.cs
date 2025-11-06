using AutoMapper;
using MyRecipeBook.Application.UseCases.Recipe.Filter;
using RDTrackR.Application.Extensions;
using RDTrackR.Communication.Requests.Recipe;
using RDTrackR.Communication.Responses.Recipe;
using RDTrackR.Domain.Dtos;
using RDTrackR.Domain.Enums;
using RDTrackR.Domain.Extensions;
using RDTrackR.Domain.Repositories.Recipe;
using RDTrackR.Domain.Services.LoggedUser;
using RDTrackR.Domain.Services.Storage;
using RDTrackR.Exceptions.ExceptionBase;

namespace MyRecipeBook.Application.UseCases.Recipe.GetById
{
    public class FilterRecipeUseCase : IFilterRecipeUseCase
    {
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly IRecipeReadOnlyRepository _repository;
        private readonly IBlobStorageService _blobStorageService;

        public FilterRecipeUseCase(IMapper mapper, ILoggedUser loggedUser, IRecipeReadOnlyRepository repository, IBlobStorageService blobStorageService)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
            _repository = repository;
            _blobStorageService = blobStorageService;
        }
        public async Task<ResponseRecipesJson> Execute(RequestFilterRecipeJson request)
        {
            Validate(request);
            var loggedUser = await _loggedUser.User();

            var filters = new FilterRecipesDto
            {
                RecipeTitle_Ingredient = request.RecipeTitle_Ingredient,
                CookingTimes = request.CookingTimes.Distinct().Select(c => (CookingTime)c).ToList(),
                Difficulties = request.Difficulties.Distinct().Select(c => (Difficulty)c).ToList(),
                DishTypes = request.DishTypes.Distinct().Select(c => (DishType)c).ToList(),
            };
            var recipes = await _repository.Filter(loggedUser, filters);

            return new ResponseRecipesJson
            {
                Recipes = await recipes.MapToShortRecipeJson(loggedUser, _blobStorageService, _mapper)
            };
        }

        private static void Validate(RequestFilterRecipeJson request)
        {
            var validate = new FilterRecipeValidator();

            var result = validate.Validate(request);

            if (result.IsValid.IsFalse())
            {
                var errorMessages = result.Errors
                    .Select(e => e.ErrorMessage).Distinct().ToList();
                throw new ErrorOnValidationException(errorMessages);
            }

        }
    }
}
