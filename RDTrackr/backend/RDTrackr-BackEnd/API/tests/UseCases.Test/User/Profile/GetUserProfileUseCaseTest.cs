using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using MyRecipeBook.Application.UseCases.User.Profile;
using Shouldly;

namespace UseCases.Test.User.Profile
{
    public class GetUserProfileUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, var _) = UserBuilder.Build();

            var userCase = CreateUseCase(user);

            var result = await userCase.Execute();

            result.ShouldNotBeNull();
            result.Name.ShouldBe(user.Name);
            result.Email.ShouldBe(user.Email);
        }

        public static GetUserProfileUseCase CreateUseCase(RDTrackR.Domain.Entities.User user)
        {
            var mapper = MapperBuilder.Build();
            var loggerUsed = LoggedUserBuilder.Build(user);

            return new GetUserProfileUseCase(loggerUsed, mapper);
        }
    }
}
