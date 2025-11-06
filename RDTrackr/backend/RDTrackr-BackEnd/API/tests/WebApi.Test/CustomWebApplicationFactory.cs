using CommonTestUtilities.BlobStorage;
using CommonTestUtilities.Entities;
using CommonTestUtilities.IdEncryption;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RDTrackR.Domain.Entities;
using RDTrackR.Domain.Enums;
using RDTrackR.Infrastructure.DataAccess;


namespace WebApi.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private RDTrackR.Domain.Entities.Recipe _recipe = default!;
        private RDTrackR.Domain.Entities.User _user = default!;
        private string _password = string.Empty;
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test")
                .ConfigureServices(services => 
                {
                    var descriptor = services.SingleOrDefault(
                        descriptor => descriptor.ServiceType == typeof(DbContextOptions<RDTrackRDbContext>)
                        );
                    if(descriptor is not null)
                        services.Remove(descriptor);

                    var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                    var blobStorage = new BlobStorageServiceBuilder().Build();
                    services.AddScoped(option =>blobStorage);

                    services.AddDbContext<RDTrackRDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                        options.UseInternalServiceProvider(provider);
                    });
                    using var scope = services.BuildServiceProvider().CreateScope();

                    var dbContext = scope.ServiceProvider.GetRequiredService<RDTrackRDbContext>();
                    dbContext.Database.EnsureDeleted();

                    StartDatabase(dbContext);

                } ) ;
        }

        public string GetEmail() => _user.Email;
        public Guid GetUserIdentifier() => _user.UserIdentifier;
        public string GetPassword() => _password;
        public string GetName() => _user.Name;

        public string GetRecipeId() => IdEncripterBuilder.Build().Encode(_recipe.Id);
        public string GetRecipeTitle() => _recipe.Title;
        public Difficulty GetRecipeDifficulty() => _recipe.Difficulty!.Value;
        public CookingTime GetRecipeCookingTime() => _recipe.CookingTime!.Value;
        public IList<RDTrackR.Domain.Enums.DishType> GetDishTypes() => _recipe.DishTypes.Select(c => c.Type).ToList();
        private void StartDatabase(RDTrackRDbContext dbContext)
        {
            (_user, _password) = UserBuilder.Build();

            _recipe = RecipeBuilder.Build(_user);


            dbContext.Users.Add(_user);

            dbContext.Recipes.Add(_recipe);


            dbContext.SaveChanges();
        }
    }

   
}
