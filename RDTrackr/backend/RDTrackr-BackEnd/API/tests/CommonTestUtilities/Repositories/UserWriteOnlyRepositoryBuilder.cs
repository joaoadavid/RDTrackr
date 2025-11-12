using Moq;
using RDTrackR.Domain.Repositories.Users;

namespace CommonTestUtilities.Repositories
{
    public static class UserWriteOnlyRepositoryBuilder
    {
        public static IUserWriteOnlyRepository Build()
        {
            var mock = new Mock<IUserWriteOnlyRepository>();

            return mock.Object;
        }
    }
}
