using Moq;
using RDTrackR.Domain.Entities;
using RDTrackR.Domain.Services.LoggedUser;

namespace CommonTestUtilities.LoggedUser
{
    public static class LoggedUserBuilder
    {
        public static ILoggedUser Build(User user)
        {
            var mock = new Mock<ILoggedUser>();
            mock.Setup( x=>x.User()).ReturnsAsync(user);

            return mock.Object;
        }
    }
}
