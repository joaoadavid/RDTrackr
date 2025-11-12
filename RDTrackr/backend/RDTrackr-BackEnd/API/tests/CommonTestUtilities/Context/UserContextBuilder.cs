using Moq;
using RDTrackR.Domain.Context;

namespace CommonTestUtilities.Context
{
    public static class UserContextBuilder
    {
        public static IUserContext Build(long userId, string userName)
        {
            var mock = new Mock<IUserContext>();
            mock.SetupGet(u => u.UserId).Returns(userId);
            mock.SetupGet(u => u.UserName).Returns(userName);
            return mock.Object;
        }
    }
}
