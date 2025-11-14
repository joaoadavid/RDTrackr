using Moq;
using RDTrackR.Domain.Context;

namespace CommonTestUtilities.Context
{
    public static class UserContextBuilder
    {
        public static IUserContext Build(long userIdentifier, string name, string role = "Admin")
        {
            var mock = new Mock<IUserContext>();

            mock.Setup(u => u.UserId).Returns(userIdentifier);
            mock.Setup(u => u.UserName).Returns(name);
            mock.Setup(u => u.Role).Returns(role);

            return mock.Object;
        }

    }
}
