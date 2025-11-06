using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RDTrackR.Domain.Dtos;
using RDTrackR.Domain.Entities;
using RDTrackR.Domain.Extensions;
using RDTrackR.Domain.Repositories.Recipe;

namespace RDTrackR.Infrastructure.DataAccess.Repositories
{
    public class RecipeRepository : IRecipeWriteOnlyRepository, IRecipeReadOnlyRepository, IRecipeUpdateOnlyRepository
    {
        private readonly RDTrackRDbContext _dbContext;

        public RecipeRepository(RDTrackRDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Recipe recipe) => await _dbContext.Recipes.AddAsync(recipe);

        public async Task<IList<Recipe>> Filter(User user, FilterRecipesDto filters)
        {
            //prepara a query para consultar no banco
            var query = _dbContext.Recipes.AsNoTracking()//não permite atualizar os dados das receitas;
                .Include(recipe => recipe.Ingredients)//inclui os ingredientes na consulta fazendo join
                .Where(recipe => recipe.Active && recipe.UserId == user.Id);

            if (filters.Difficulties.Any())
            {
                query = query.Where(recipe => recipe.Difficulty.HasValue &&
                filters.Difficulties.Contains(recipe.Difficulty.Value));
            }

            if (filters.CookingTimes.Any())
            {
                query = query.Where(recipe => recipe.CookingTime.HasValue &&
                filters.CookingTimes.Contains(recipe.CookingTime.Value));
            }

            if (filters.DishTypes.Any())
            {
                query = query.Where(recipe => recipe.DishTypes.Any(
                    dishType => filters.DishTypes.Contains(dishType.Type)));
            }

            if (filters.RecipeTitle_Ingredient.NotEmpty())
            {
                query = query.Where(recipe =>
                    recipe.Title.Contains(filters.RecipeTitle_Ingredient) ||
                    recipe.Ingredients.Any(ingredient =>
                        ingredient.Item.Contains(filters.RecipeTitle_Ingredient))
                );
            }

            return await query.ToListAsync();
        }

        async Task<Recipe?> IRecipeReadOnlyRepository.GetById(User user, long recipeId)
        {
            return await GetFullRecipe()
                .AsNoTracking()
                .FirstOrDefaultAsync(recipe =>
                    recipe.Id == recipeId &&
                    recipe.Active &&
                    recipe.UserId == user.Id);
        }
        async Task<Recipe?> IRecipeUpdateOnlyRepository.GetById(User user, long recipeId)
        {
            return await GetFullRecipe()
                .FirstOrDefaultAsync(recipe =>
                    recipe.Id == recipeId &&
                    recipe.Active &&
                    recipe.UserId == user.Id);

        }

        public async Task Delete(long recipeId)
        {
            var recipe = await _dbContext.Recipes.FindAsync(recipeId);

            _dbContext.Recipes.Remove(recipe!);
        }

        public void Update(Recipe recipe) => _dbContext.Recipes.Update(recipe);

        private IIncludableQueryable<Recipe, IList<DishType>> GetFullRecipe()
        {
            return _dbContext
                 .Recipes
                .Include(recipe => recipe.Ingredients)
                .Include(recipe => recipe.Instructions)
                .Include(recipe => recipe.DishTypes);
        }

        public async Task<IList<Recipe>> GetForDashboard(User user)
        {
            return await _dbContext
                .Recipes
                .AsNoTracking()
                .Include(recipe => recipe.Ingredients)
                .Where(recipe => recipe.Active && recipe.UserId == user.Id)
                .OrderByDescending(recipe => recipe.CreatedOn)
                .Take(5)
                .ToListAsync();
        }
    }
}
