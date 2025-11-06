using CommonTestUtilities.BlobStorage;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using Microsoft.AspNetCore.Http;
using MyRecipeBook.Application.UseCases.Recipe;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;
using Shouldly;
using UseCases.Test.Recipes.InlineDatas;

namespace UseCases.Test.Recipes.Register
{
    public class RegisterRecipeUseCaseTest
    {
        [Fact]
        public async Task Success_Without_Image()
        {
            (var user, _) = UserBuilder.Build();

            var request = RequestRegisterRecipeFormDataBuilder.Build();

            var useCase = CreateUseCase(user);

            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.Id.ShouldNotBeNullOrWhiteSpace();
            result.Title.ShouldBe(request.Title);
        }

        [Theory]
        [ClassData(typeof(ImageTypesInlineData))]
        public async Task Success_With_Image(IFormFile file)
        {
            (var user, _) = UserBuilder.Build();

            var request = RequestRegisterRecipeFormDataBuilder.Build(file);

            var useCase = CreateUseCase(user);

            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.Id.ShouldNotBeNullOrWhiteSpace();
            result.Title.ShouldBe(request.Title);
        }

        [Fact]
        public async Task Error_Title_Empty()
        {
            (var user, _) = UserBuilder.Build();

            var request = RequestRegisterRecipeFormDataBuilder.Build();
            request.Title = string.Empty;

            var useCase = CreateUseCase(user);

            Func<Task> act = async () => { await useCase.Execute(request); };

            var errors = await act.ShouldThrowAsync<ErrorOnValidationException>();
            errors.GetErrorMessages().ShouldContain(ResourceMessagesException.RECIPE_TITLE_EMPTY);
                
        }

        [Fact]
        public async Task Error_Invalid_File()
        {
            (var user, _) = UserBuilder.Build();

            var textFile = FormFileBuilder.Txt();

            var request = RequestRegisterRecipeFormDataBuilder.Build(textFile);

            var useCase = CreateUseCase(user);

            var act = async () => { await useCase.Execute(request); };

            var errors = await act.ShouldThrowAsync<ErrorOnValidationException>();
            errors.GetErrorMessages().Count().ShouldBe(1);
            errors.GetErrorMessages().ShouldContain(ResourceMessagesException.ONLY_IMAGES_ACCEPTED);
        }

        private static RegisterRecipeUseCase CreateUseCase(RDTrackR.Domain.Entities.User user)
        {
            var mapper = MapperBuilder.Build();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var repository = RecipeWriteOnlyRepositoryBuilder.Build();
            var blobStorage = new BlobStorageServiceBuilder().Build();

            return new RegisterRecipeUseCase(repository, loggedUser,  unitOfWork, blobStorage, mapper);
        }
    }
}
