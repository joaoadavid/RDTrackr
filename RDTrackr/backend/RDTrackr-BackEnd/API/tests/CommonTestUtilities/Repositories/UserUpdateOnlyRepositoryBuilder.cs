using Moq;
using MyRecipeBook.Domain.Repositories.User;
using RDTrackR.Domain.Entities;

namespace CommonTestUtilities.Repositories
{
    public class UserUpdateOnlyRepositoryBuilder
    {
        private readonly Mock<IUserUpdateOnlyRepository> _userUpdateOnlyRepository;
        public UserUpdateOnlyRepositoryBuilder()
        {
            _userUpdateOnlyRepository = new Mock<IUserUpdateOnlyRepository>();
        }

        public UserUpdateOnlyRepositoryBuilder GetById(User user)
        {
            _userUpdateOnlyRepository.Setup(
                repository => repository.GetById(user.Id))
                .ReturnsAsync(user);
            return this;         
        }

        public IUserUpdateOnlyRepository Build()
        {
            return _userUpdateOnlyRepository.Object;
        }
    }
}
